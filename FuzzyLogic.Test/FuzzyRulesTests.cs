using NUnit.Framework.Constraints;

namespace FuzzyLogic.Test;

public partial class FuzzyRulesTests
{
    private const double Epsilon = 0.01;

    [TestCase(0, 0)]
    [TestCase(0.15, 1)]
    [TestCase(0.25, 1)]
    [TestCase(0.5, 0)]
    [TestCase(1, 0)]
    [TestCase(2, 0)]
    [TestCase(-1, 0)]
    [TestCase(double.MinValue, 0)]
    [TestCase(double.MaxValue, 0)]
    [TestCase(double.NaN, double.NaN)]
    [TestCase(double.NegativeInfinity, double.NaN)]
    [TestCase(double.PositiveInfinity, double.NaN)]
    public void Single_Antecedent_and_Consequent(double health, double expectedRetreat)
    {
        // Arrange
        FuzzyRule.If(Health.Is(Low))
            .Then(Retreat.Is(High));
        
        _health = health;
        
        // Act
        var retreat = Retreat.Evaluate();
        
        // Assert
        Assert.That(retreat, Is.EqualTo(expectedRetreat));
    }

    [TestCase(0, 0, 0)]
    [TestCase(1, 2, 1)]
    [TestCase(0.5, 2, 0)]
    [TestCase(0.9, 2, 0)]
    [TestCase(1, 0, 0)]
    [TestCase(1, 10, 0)]
    public void Multiple_Antecedents(double health, double enemiesNearby, double expectedFight)
    {
        // Arrange
        FuzzyRule.If(Health.Is(Max))
                .And(EnemiesNearby.Is(Low))
            .Then(Fight.Is(High));
        
        _health = health;
        _enemiesNearby = enemiesNearby;
        
        // Act
        var fight = Fight.Evaluate();
        
        // Assert
        Assert.That(fight, Is.EqualTo(expectedFight));
    }

    [TestCase(0, 1)]
    [TestCase(0.15, 1)]
    [TestCase(0.25, 0)]
    [TestCase(0.5, 1)]
    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(-1, 1)]
    [TestCase(double.MinValue, 1)]
    [TestCase(double.MaxValue, 1)]
    [TestCase(double.NaN, double.NaN)]
    [TestCase(double.NegativeInfinity, double.NaN)]
    [TestCase(double.PositiveInfinity, double.NaN)]
    public void Negation(double health, double expectedRetreat)
    {
        // Arrange
        FuzzyRule.If(Health.IsNot(Low))
            .Then(Retreat.Is(High));
        
        _health = health;
        
        // Act
        var retreat = Retreat.Evaluate();
        
        // Assert
        Assert.That(retreat, Is.EqualTo(expectedRetreat));
    }

    [TestCase(0, 1)]
    [TestCase(0.25, 0.5)]
    [TestCase(0.5, 0.0)]
    [TestCase(0.9, 0)]
    [TestCase(1, 0)]
    public void Multiple_Rules(double health, double expectedRetreat)
    {
        // Arrange
        FuzzyRule.If(Health.Is(Critical)).Then(Retreat.Is(High));
        FuzzyRule.If(Health.Is(Low)).Then(Retreat.Is(Medium));
        FuzzyRule.If(Health.Is(Medium)).Then(Retreat.Is(Low));
        
        _health = health;
        
        // Act
        var retreat = Retreat.Evaluate();
        
        // Assert
        Assert.That(retreat, IsCloseTo(expectedRetreat));
    }

    [TestCase(0, 1, 0)]
    [TestCase(0.125, 1, 0)]
    [TestCase(0.25, 1, 0)]
    [TestCase(0.375, 0.5, 0.5)]
    [TestCase(0.5, 0, 1)]
    [TestCase(1, 0, 1)]
    public void Multiple_Consequents(double health, double expectedRetreat, double expectedFight)
    {
        // Arrange
        FuzzyRule.If(Health.IsNot(Low))
            .Then(Fight.Is(High))
                .And(Retreat.Is(Low));
        
        FuzzyRule.If(Health.Is(Low))
            .Then(Retreat.Is(High))
                .And(Fight.Is(Low));
        
        _health = health;
        
        // Act
        var retreat = Retreat.Evaluate();
        var fight = Fight.Evaluate();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(retreat, Is.EqualTo(expectedRetreat), "Retreat: {0} != {1}", retreat, expectedRetreat);
            Assert.That(fight, Is.EqualTo(expectedFight), "Fight: {0} != {1}", fight, expectedFight);
        });
    }

    [TestCase(0, 0, 0.5, 0)]
    [TestCase(0.25, 0, 0.5, 0)]
    [TestCase(0.5, 0, 0, 0.5)]
    [TestCase(1, 0, 0, 0.5)]
    [TestCase(0, 10, 0.875, 0)]
    [TestCase(0.25, 10, 0.875, 0)]
    [TestCase(0.5, 10, 0.5, 0.125)]
    [TestCase(1, 10, 0.5, 0.125)]
    public void Complex_Fuzzy_Rule_Combinations(double health, double enemiesNearby, double expectedRetreat, double expectedFight)
    {
        // Arrange
        FuzzyRule.If(Health.Is(Medium))
                .Or(Health.Is(High))
            .Then(Fight.Is(Medium))
                .And(Retreat.Is(Low))
            .Else(Retreat.Is(Medium))
                .And(Fight.Is(Low));
        
        FuzzyRule.If(EnemiesNearby.Is(High))
            .Then(Retreat.Is(High))
                .And(Fight.Is(Low));
        
        _health = health;
        _enemiesNearby = enemiesNearby;

        // Act
        var retreat = Retreat.Evaluate();
        var fight = Fight.Evaluate();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(retreat, Is.EqualTo(expectedRetreat), "Retreat: {0} != {1}", retreat, expectedRetreat);
            Assert.That(fight, Is.EqualTo(expectedFight), "Fight: {0} != {1}", fight, expectedFight);
        });
    }

    private static RangeConstraint IsCloseTo(double expected)
    {
        var expectedMin = expected - Epsilon;
        var expectedMax = expected + Epsilon;
        
        return Is.InRange(expectedMin, expectedMax);
    }

    /* 
    [Test(Description = "Membership Grades")]
    public void Syntax_3()
    {
        FuzzyRule.If(Health.Is.Critical).And(ThreatLevel.Is.Moderate).Then(RetreatGoal.Is.Defend);
    }

    [Test(Description = "Fuzzy Quantifiers")]
    public void Syntax_4()
    {
        FuzzyRule.If(AtLeast(2, EnemiesNearby)).And(AmmoLevel.Is.High).Then(RetreatGoal.Is.Engage);
    }

    [Test(Description = "Hedges")]
    public void Syntax_5()
    {
        FuzzyRule.If(Health.Is.Somewhat.Low).And(AlliesEngaged.Is.FairlyHigh).Then(RetreatGoal.Is.Assist);
    }

    [Test(Description = "Compound Antecedent")]
    public void Syntax_7()
    {
        FuzzyRule.If((Health.Is.Low).Or(Ammo.Is.Low)).And(EnemiesNearby.Is.High).Then(RetreatGoal.Is.EvasiveManeuvers);
    }

    [Test(Description = "Implication Words")]
    public void Syntax_8()
    {
        FuzzyRule.If(ThreatLevel.Is.High).Then(RetreatGoal.Is.SeekCover);
    }

    [Test(Description = "Chained Rules")]
    public void Syntax_9()
    {
        FuzzyRule.If(AllyNeedsHelp).Then(RetreatGoal.Is.Assist).ElseIf(ThreatLevel.Is.VeryHigh).Then(RetreatGoal.Is.Retreat);

    [Test(Description = "Weighted Rules")]
    public void Syntax_10()
    {
        FuzzyRule.If(AlliesEngaged.Is.High).Then(RetreatGoal.Is.ProvideHeavySupport).WithWeight(0.8);
        FuzzyRule.If(AlliesEngaged.Is.Low).Then(RetreatGoal.Is.ProvideLightSupport).WithWeight(0.2);
    }

    [Test(Description = "T-Norm and S-Norm Operators")]
    public void Syntax_11()
    {
        FuzzyRule.If((Health.Is.Low).And(Ammo.Is.Low)).Or(EnemiesNearby.Is.VeryHigh).Then(RetreatGoal.Is.Fallback);
    }

    [Test(Description = "Fuzzy Set Operators")]
    public void Syntax_12()
    {
        FuzzyRule.If(Health.Is.Poor).And(ThreatLevel.Is.NotLow).Then(RetreatGoal.Is.Defend);
    }
*/
}