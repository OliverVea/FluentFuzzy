using FluentFuzzy.MemberFunctions;

namespace FluentFuzzy.Test;

public partial class FuzzyRuleTests
{
    private static int _next;
    private static int Next => _next++;

    private int None { get; } = Next;
    private int Critical { get; } = Next;
    private int Low { get; } = Next;
    private int Medium { get; } = Next;
    private int High { get; } = Next;
    private int Max { get; } = Next;
    private int AtLeast2 { get; } = Next;
    private int VeryHigh { get; } = Next;
    
    private FuzzyInput Health { get; set; } = null!;
    private FuzzyInput Ammo { get; set; } = null!;
    private FuzzyInput EnemiesNearby { get; set; } = null!;
    
    private FuzzyOutput Retreat { get; set; } = null!;
    private FuzzyOutput Fight { get; set; } = null!;

    private double _health;
    private int _enemiesNearby;
    private int _ammo;
    
    [SetUp]
    public void SetUp()
    {
        Health = new FuzzyInput(() => _health);
        Health.Set(Critical, new Trapezoid(0, 0, 0.05, 0.2));
        Health.Set(Low, new Trapezoid(0, 0, 0.25, 0.5));
        Health.Set(Medium, new Triangle(0.25, 0.5, 0.75));
        Health.Set(High, new Trapezoid(0.5, 0.75, 1, 1));
        Health.Set(Max, new Trapezoid(0.99, 0.99, 1.01, 1.01));

        Ammo = new FuzzyInput(() => _ammo);
        Ammo.Set(Low, new Trapezoid(0, 0, 15, 25));
        
        EnemiesNearby = new FuzzyInput(() => _enemiesNearby);
        EnemiesNearby.Set(None, new Value(0));
        EnemiesNearby.Set(Low, new Trapezoid(1, 1, 2, 5));
        EnemiesNearby.Set(Medium, new Triangle(2, 5, 7));
        EnemiesNearby.Set(High, new Trapezoid(5, 7, double.MaxValue, double.MaxValue));
        EnemiesNearby.Set(AtLeast2, new AtLeast(2));
        EnemiesNearby.Set(VeryHigh, new Trapezoid(8, 10, double.MaxValue, double.MaxValue));
        
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