using FuzzyLogic.MemberFunctions;

namespace FuzzyLogic.Test;

// ReSharper disable once InconsistentNaming
public class TestUT
{
    private const int None = 0;
    private const int Critical = 1;
    private const int Low = 2;
    private const int Medium = 3;
    private const int High = 4;
    private const int Max = 5;

    private static double _health = 0.25;
    private static double _enemiesNearby = 0.25;
    
    // ReSharper disable InconsistentNaming
    private FuzzyInput Health = null!;
    private FuzzyInput EnemiesNearby = null!;
    
    private FuzzyOutput Retreat = null!;
    private FuzzyOutput Fight = null!;
    // ReSharper restore InconsistentNaming

    [SetUp]
    public void SetUp()
    {
        Health = new(() => _health);
        Health.Set(Critical, new Trapezoid(0, 0, 0.05, 0.2));
        Health.Set(Low, new Triangle(0, 0.25, 0.5));
        Health.Set(Medium, new Triangle(0.25, 0.5, 0.75));
        Health.Set(High, new Trapezoid(0.5, 0.75, 1, 1));
        Health.Set(Max, new Trapezoid(0.99, 0.99, 1.01, 1.01));
        
        EnemiesNearby = new(() => _enemiesNearby);
        EnemiesNearby.Set(None, new Value(0));
        EnemiesNearby.Set(Low, new Trapezoid(1, 1, 2, 5));
        EnemiesNearby.Set(Medium, new Triangle(2, 5, 7));
        EnemiesNearby.Set(High, new Trapezoid(5, 7, double.MaxValue, double.MaxValue));
        
        Retreat = new();
        Retreat.Set(Low, new Trapezoid(0, 0, 0.25, 0.5));
        Retreat.Set(Medium, new Triangle(0.25, 0.5, 0.75));
        Retreat.Set(High, new Trapezoid(0.5, 0.75, 1.25, 1.5));
        
        Fight = new();
        Fight.Set(Low, new Trapezoid(0, 0, 0.25, 0.5));
        Fight.Set(Medium, new Triangle(0.25, 0.5, 0.75));
        Fight.Set(High, new Trapezoid(0.5, 0.75, 1.25, 1.5));
    }
    
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
        FuzzyRule.If(Health.Is(Low)).Then(Retreat.Is(High));
        
        _health = health;
        Assert.That(Retreat.Evaluate(), Is.EqualTo(expectedRetreat));
    }

    [TestCase(0, 0, 0)]
    [TestCase(1, 2, 1)]
    [TestCase(0.5, 2, 0)]
    [TestCase(0.9, 2, 0)]
    [TestCase(1, 0, 0)]
    [TestCase(1, 10, 0)]
    public void Multiple_Antecedents(double health, double enemiesNearby, double expectedFight)
    {
        FuzzyRule.If(Health.Is(Max)).And(EnemiesNearby.Is(Low)).Then(Fight.Is(High));
        
        _health = health;
        _enemiesNearby = enemiesNearby;
        Assert.That(Fight.Evaluate(), Is.EqualTo(expectedFight));
    }

    [TestCase(0, 1)]
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
        FuzzyRule.If(!Health.Is(Low)).Then(Retreat.Is(High));
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