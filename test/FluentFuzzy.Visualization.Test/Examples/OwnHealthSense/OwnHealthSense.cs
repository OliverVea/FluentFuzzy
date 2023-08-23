using FuzzyLogic.MemberFunctions;

namespace FuzzyLogic.Visualization.Test.Examples.OwnHealthSense;

public class OwnHealthSense
{
    public readonly FuzzyInput Percentage;
    public readonly FuzzyInput Absolute;
    public readonly FuzzyOutput Health = new();

    public const int Critical = 0;
    public const int Low = 1;
    public const int Medium = 2;
    public const int High = 3;
    public const int Max = 4;
    public const int None = 5;
    
    public OwnHealthSense(Func<double> healthPercentageFunction, Func<double> absoluteHealthFunction)
    {
        Percentage = new FuzzyInput(healthPercentageFunction);
        Absolute = new FuzzyInput(absoluteHealthFunction);

        SetupMembershipFunctions();
        SetupRules();
    }

    private void SetupMembershipFunctions()
    {
        Percentage.Set(None, new Value(0, 0.002));
        Percentage.Set(Critical, new Trapezoid(0, 0, 0.05, 0.15));
        Percentage.Set(Low, new Trapezoid(0, 0,0.2, 0.5));
        Percentage.Set(Medium, new Trapezoid(0.2, 0.5, 0.7, 1));
        Percentage.Set(High, new Triangle(0.7, 1-0.002, 1 - 0.002));
        Percentage.Set(Max, new Value(1, 0.002));
        
        Absolute.Set(Low, new Line(200, 0));
        Absolute.Set(Medium, new Triangle(0, 200, 500));
        Absolute.Set(High, new Line(200, 500));
        
        Health.Set(None, new Triangle(-0.5, 0, 0.5));
        Health.Set(Low, new Triangle(-0.2, 0.0, 0.2));
        Health.Set(Medium, new Triangle(0.3, 0.5, 0.7));
        Health.Set(High, new Triangle(0.8, 1, 1.2));
        Health.Set(Max, new Triangle(0.5, 1, 1.5));
    }

    private void SetupRules()
    {
        FuzzyRule.If(Percentage.Is(Max))
            .Then(Health.Is(Max))
            .ElseIf(Absolute.Is(High))
            .Then(Health.Is(High));

        FuzzyRule.If(Percentage.Is(None))
            .Then(Health.Is(None))
            .ElseIf(Absolute.Is(Low))
            .Then(Health.Is(Low));

        FuzzyRule.If(Percentage.Is(Low))
            .Then(Health.Is(Low));

        FuzzyRule.If(Percentage.Is(Medium))
                .Or(Absolute.Is(Medium))
            .Then(Health.Is(Medium));

        FuzzyRule.If(Percentage.Is(High))
            .Then(Health.Is(High));
    }
}