using FuzzyLogic.MemberFunctions;

namespace FuzzyLogic.Test;

public partial class FuzzyRulesTests
{
    private const int None = 0;
    private const int Critical = 1;
    private const int Low = 2;
    private const int Medium = 3;
    private const int High = 4;
    private const int Max = 5;
    
    private FuzzyInput Health { get; set; } = null!;
    private FuzzyInput EnemiesNearby { get; set; } = null!;
    
    private FuzzyOutput Retreat { get; set; } = null!;
    private FuzzyOutput Fight { get; set; } = null!;

    private double _health;
    private double _enemiesNearby;
    
    [SetUp]
    public void SetUp()
    {
        Health = new FuzzyInput(() => _health);
        Health.Set(Critical, new Trapezoid(0, 0, 0.05, 0.2));
        Health.Set(Low, new Trapezoid(0, 0, 0.25, 0.5));
        Health.Set(Medium, new Triangle(0.25, 0.5, 0.75));
        Health.Set(High, new Trapezoid(0.5, 0.75, 1, 1));
        Health.Set(Max, new Trapezoid(0.99, 0.99, 1.01, 1.01));
        
        EnemiesNearby = new FuzzyInput(() => _enemiesNearby);
        EnemiesNearby.Set(None, new Value(0));
        EnemiesNearby.Set(Low, new Trapezoid(1, 1, 2, 5));
        EnemiesNearby.Set(Medium, new Triangle(2, 5, 7));
        EnemiesNearby.Set(High, new Trapezoid(5, 7, double.MaxValue, double.MaxValue));
        
        Retreat = new FuzzyOutput();
        Retreat.Set(Low, new Trapezoid(-0.5, -0.25, 0.25, 0.5));
        Retreat.Set(Medium, new Triangle(0.25, 0.5, 0.75));
        Retreat.Set(High, new Trapezoid(0.5, 0.75, 1.25, 1.5));
        
        Fight = new FuzzyOutput();
        Fight.Set(Low, new Trapezoid(-0.5, -0.25, 0.25, 0.5));
        Fight.Set(Medium, new Triangle(0.25, 0.5, 0.75));
        Fight.Set(High, new Trapezoid(0.5, 0.75, 1.25, 1.5));
    }
}