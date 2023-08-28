using FluentFuzzy.MemberFunctions;

namespace FluentFuzzy.Test.MemberFunctions;

public class LineTests
{
    [TestCase(false, 0, 0)]
    [TestCase(false, 0.25, 0)]
    [TestCase(false, 0.5, 0.5)]
    [TestCase(false, 0.75, 1)]
    [TestCase(false, 1, 1)]
    [TestCase(false, double.MinValue, 0)]
    [TestCase(false, double.MaxValue, 1)]
    [TestCase(false, double.NegativeInfinity, 0)]
    [TestCase(false, double.PositiveInfinity, 1)]
    [TestCase(true, 0, 1)]
    [TestCase(true, 0.25, 1)]
    [TestCase(true, 0.5, 0.5)]
    [TestCase(true, 0.75, 0)]
    [TestCase(true, 1, 0)]
    [TestCase(true, double.MinValue, 1)]
    [TestCase(true, double.MaxValue, 0)]
    [TestCase(true, double.NegativeInfinity, 1)]
    [TestCase(true, double.PositiveInfinity, 0)]
    public void Evaluate(bool reversed, double x, double expected)
    {
        // Arrange
        var mf = reversed ? new Line(0.75, 0.25) : new Line(0.25, 0.75);
        
        // Act
        var actual = mf.Evaluate(x);
        
        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}