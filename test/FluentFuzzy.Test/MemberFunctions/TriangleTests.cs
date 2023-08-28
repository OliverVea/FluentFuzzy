using FluentFuzzy.MemberFunctions;

namespace FluentFuzzy.Test.MemberFunctions;

public class TriangleTests
{
    [TestCase(0, 0)]
    [TestCase(0.25, 0)]
    [TestCase(0.375, 0.5)]
    [TestCase(0.5, 1)]
    [TestCase(0.625, 0.5)]
    [TestCase(0.75, 0)]
    [TestCase(1, 0)]
    [TestCase(double.MinValue, 0)]
    [TestCase(double.MaxValue, 0)]
    [TestCase(double.NegativeInfinity, double.NaN)]
    [TestCase(double.PositiveInfinity, double.NaN)]
    public void Evaluate(double x, double expected)
    {
        // Arrange
        var mf = new Triangle(0.25, 0.5, 0.75);
        
        // Act
        var actual = mf.Evaluate(x);
        
        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}