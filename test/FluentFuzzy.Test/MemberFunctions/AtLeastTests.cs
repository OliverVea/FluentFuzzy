using FluentFuzzy.MemberFunctions;

namespace FluentFuzzy.Test.MemberFunctions;

public class AtLeastTests
{
    [TestCase(0, 0)]
    [TestCase(0.499, 0)]
    [TestCase(0.5, 1)]
    [TestCase(1, 1)]
    [TestCase(double.MinValue, 0)]
    [TestCase(double.MaxValue, 1)]
    [TestCase(double.NegativeInfinity, 0)]
    [TestCase(double.PositiveInfinity, 1)]
    public void Evaluate(double x, double expected)
    {
        // Arrange
        var mf = new AtLeast(0.5);
        
        // Act
        var actual = mf.Evaluate(x);
        
        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}