using FuzzyLogic.MemberFunctions;

namespace FuzzyLogic.Visualization.Test;

public partial class FuzzyInputPlotsTests
{
    private const int Critical = 1;
    private const int Low = 2;
    private const int Medium = 3;
    private const int High = 4;
    private const int Max = 5;
    
    private FuzzyInput Health { get; set; } = null!;
    
    [SetUp]
    public void SetUp()
    {
        Health = new FuzzyInput(() => 0.0);
        Health.Set(Critical, new Trapezoid(0, 0, 0.05, 0.2));
        Health.Set(Low, new Trapezoid(0, 0, 0.25, 0.5));
        Health.Set(Medium, new Triangle(0.25, 0.5, 0.75));
        Health.Set(High, new Trapezoid(0.5, 0.75, 1, 1));
        Health.Set(Max, new Trapezoid(0.99, 0.99, 1.01, 1.01));
    }
}