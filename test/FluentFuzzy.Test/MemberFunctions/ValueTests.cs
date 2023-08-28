using FluentFuzzy.MemberFunctions;

namespace FuzzyLogic.Test.MemberFunctions;

public class ValueTests
{
    private const double Tolerance = 0.01;
    
    [TestCase(0, 0)]
    [TestCase(0.5 - 1.5 * Tolerance, 0)]
    [TestCase(0.5 - 0.5 * Tolerance, 1)]
    [TestCase(0.5, 1)]
    [TestCase(0.5 + 0.5 * Tolerance, 1)]
    [TestCase(0.5 + 1.5 * Tolerance, 0)]
    [TestCase(1, 0)]
    [TestCase(double.MinValue, 0)]
    [TestCase(double.MaxValue, 0)]
    [TestCase(double.NegativeInfinity, 0)]
    [TestCase(double.PositiveInfinity, 0)]
    public void Evaluate(double x, double expected)
    {
        // Arrange
        var mf = new Value(0.5, Tolerance);
        
        // Act
        var actual = mf.Evaluate(x);
        
        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}