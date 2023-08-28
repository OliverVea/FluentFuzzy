using NUnit.Framework.Constraints;

namespace FluentFuzzy.Test;

public partial class FuzzyRuleTests
{
    private const double Epsilon = 0.01;

    [TestCase(0, 1)]
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
    public void Multiple_Antecedents(double health, int enemiesNearby, double expectedFight)
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

    [TestCase(0, 0)]
    [TestCase(0.15, 0)]
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

    [TestCase(0, 0.875)]
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
    public void Complex_Fuzzy_Rule_Combinations(double health, int enemiesNearby, double expectedRetreat, double expectedFight)
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

    [TestCase(0, 0, 1)]
    [TestCase(0, 2, 1)]
    [TestCase(0, 5, 1)]
    [TestCase(0.25, 0, 1)]
    [TestCase(0.25, 2, 1)]
    [TestCase(0.25, 5, 1)]
    [TestCase(0.5, 0, 1)]
    [TestCase(0.5, 2, 0)]
    [TestCase(0.5, 5, 0)]
    public void Fuzzy_Quantifiers(double health, int enemiesNearby, double expectedRetreat)
    {
        // Arrange
        FuzzyRule.If(EnemiesNearby.Is(AtLeast2))
                .And(Health.IsNot(Low))
            .Then(Retreat.Is(Low))
            .Else(Retreat.Is(High));
        
        _health = health;
        _enemiesNearby = enemiesNearby;

        // Act
        var retreat = Retreat.Evaluate();
        
        // Assert
        Assert.That(retreat, Is.EqualTo(expectedRetreat), "Retreat: {0} != {1}", retreat, expectedRetreat);
    }
    
    [TestCase(0, 0, 0, 0)]
    [TestCase(0, 20, 0, 0)]
    [TestCase(0, 100, 0, 0)]
    [TestCase(0.25, 0, 0, 0)]
    [TestCase(0.25, 20, 0, 0)]
    [TestCase(0.25, 100, 0, 0)]
    [TestCase(0.5, 0, 0, 0)]
    [TestCase(0.5, 20, 0, 0)]
    [TestCase(0.5, 100, 0, 0)]
    [TestCase(0, 0, 10, 1)]
    [TestCase(0, 20, 10, 1)]
    [TestCase(0, 100, 10, 1)]
    [TestCase(0.25, 0, 10, 1)]
    [TestCase(0.25, 20, 10, 1)]
    [TestCase(0.25, 100, 10, 1)]
    [TestCase(0.5, 0, 10, 1)]
    [TestCase(0.5, 20, 10, 1)]
    [TestCase(0.5, 100, 10, 0)]
    public void Compound_Or_Antecedent(double health, int ammo, int enemiesNearby, double expectedRetreat)
    {
        // Arrange
        FuzzyRule.If(Health.Is(Low))
                .Or(Ammo.Is(Low))
                .And(EnemiesNearby.Is(High))
            .Then(Retreat.Is(High));
        
        _health = health;
        _ammo = ammo;
        _enemiesNearby = enemiesNearby;

        // Act
        var retreat = Retreat.Evaluate();
        
        // Assert
        Assert.That(retreat, Is.EqualTo(expectedRetreat), "Retreat: {0} != {1}", retreat, expectedRetreat);
    }
    
    [TestCase(0, 0, 0, 1)]
    [TestCase(0, 20, 0, 1)]
    [TestCase(0, 100, 0, 0)]
    [TestCase(0.25, 0, 0, 1)]
    [TestCase(0.25, 20, 0, 1)]
    [TestCase(0.25, 100, 0, 0)]
    [TestCase(0.5, 0, 0, 0)]
    [TestCase(0.5, 20, 0, 0)]
    [TestCase(0.5, 100, 0, 0)]
    [TestCase(0, 0, 10, 1)]
    [TestCase(0, 20, 10, 1)]
    [TestCase(0, 100, 10, 1)]
    [TestCase(0.25, 0, 10, 1)]
    [TestCase(0.25, 20, 10, 1)]
    [TestCase(0.25, 100, 10, 1)]
    [TestCase(0.5, 0, 10, 1)]
    [TestCase(0.5, 20, 10, 1)]
    [TestCase(0.5, 100, 10, 1)]
    public void Compound_And_Antecedent(double health, int ammo, int enemiesNearby, float expectedRetreat)
    {
        FuzzyRule.If(Health.Is(Low))
                .And(Ammo.Is(Low))
                .Or(EnemiesNearby.Is(High))
            .Then(Retreat.Is(High));
        
        _health = health;
        _ammo = ammo;
        _enemiesNearby = enemiesNearby;

        // Act
        var retreat = Retreat.Evaluate();
        
        // Assert
        Assert.That(retreat, Is.EqualTo(expectedRetreat), "Retreat: {0} != {1}", retreat, expectedRetreat);
    }
    
    [TestCase(0, 0, 0, 0.5)]
    [TestCase(0.25, 0, 0, 0.5)]
    [TestCase(0.5, 0, 0, 0)]
    [TestCase(1, 0, 0, 0)]
    [TestCase(0, 1, 0, 0.5)]
    [TestCase(0.25, 1, 0, 0.5)]
    [TestCase(0.5, 1, 1, 0)]
    [TestCase(1, 1, 1, 0)]
    [TestCase(0, 10, 0, 0.5)]
    [TestCase(0.25, 10, 0, 0.5)]
    [TestCase(0.5, 10, 1, 0)]
    [TestCase(1, 10, 1, 0)]
    public void Chained_Rules(double health, int enemiesNearby, double expectedFight, double expectedRetreat)
    {
        FuzzyRule.If(Health.Is(Low))
            .Then(Retreat.Is(Medium))
            .ElseIf(EnemiesNearby.IsNot(None))
            .Then(Fight.Is(High));
        
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
}