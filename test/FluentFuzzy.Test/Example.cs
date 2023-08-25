using FuzzyLogic.MemberFunctions;

namespace FuzzyLogic.Test;

public class Example
{
    [Test]
    public void DocumentationExample()
    {
        var healthValue = 35;
        var health = new FuzzyInput(() => healthValue);
        var flee = new FuzzyOutput();
        
        var low = 0;
        var medium = 1;
        var high = 2;

        health.Set(low, new Trapezoid(0, 0, 25, 50));
        health.Set(medium, new Triangle(25, 50, 75));
        health.Set(high, new Trapezoid(50, 75, 100, 100));

        flee.Set(low, new Triangle(-0.5, 0, 0.5));
        flee.Set(medium, new Triangle(0, 0.5, 1));
        flee.Set(high, new Triangle(0.5, 1, 1.5));
        
        FuzzyRule.If(health.Is(high)).Then(flee.Is(low));
        FuzzyRule.If(health.Is(medium)).Then(flee.Is(medium));
        FuzzyRule.If(health.Is(low)).Then(flee.Is(high));

        Console.WriteLine($"flee(health: {health.Value}) = {flee.Evaluate()}"); // flee(health: 35) = 0,8

        const float tolerance = 0.01f;
        const double expected = 0.8d;
        var error = Math.Abs(flee.Evaluate() - expected);
        Assert.That(error < tolerance);
    }

}