using FuzzyLogic.MemberFunctions;

namespace FuzzyLogic.Test.MemberFunctions;

public class TrapezoidTests
{
    [TestCase(0, 0)]
    [TestCase(0.25, 0)]
    [TestCase(0.3, 0.5)]
    [TestCase(0.35, 1)]
    [TestCase(0.5, 1)]
    [TestCase(0.65, 1)]
    [TestCase(0.7, 0.5)]
    [TestCase(0.75, 0)]
    [TestCase(1, 0)]
    [TestCase(double.MinValue, 0)]
    [TestCase(double.MaxValue, 0)]
    [TestCase(double.NegativeInfinity, double.NaN)]
    [TestCase(double.PositiveInfinity, double.NaN)]
    public void Evaluate(double x, double expected)
    {
        // Arrange
        var mf = new Trapezoid(0.25, 0.35, 0.65, 0.75);
        
        // Act
        var actual = mf.Evaluate(x);
        
        // Assert
        actual = Math.Round(actual, 5);
        Assert.That(actual, Is.EqualTo(expected));
    }
}