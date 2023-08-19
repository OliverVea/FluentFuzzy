using System.Drawing;
using ScottPlot;

namespace FuzzyLogic.Visualization.Test.Examples.OwnHealthSense;

public class Tests
{
    private OwnHealthSense _ownHealthSense = null!;

    private double _absoluteHealth;
    private double _healthPercentage;
    
    [SetUp]
    public void Setup()
    {
        double AbsoluteHealthFunction() => _absoluteHealth;
        double HealthPercentageFunction() => _healthPercentage;
        
        _ownHealthSense = new OwnHealthSense(HealthPercentageFunction, AbsoluteHealthFunction);
    }
    
    const double HighHealth = 500;
    const double MediumHealth = 250;
    const double LowHealth = 100;

    [Test]
    public void Visualize()
    {
        // Arrange
        double? Get(double maxHealth, double x)
        {
            if (x < 0 || x > 1) return null;
            
            _healthPercentage = x;
            _absoluteHealth = _healthPercentage * maxHealth;
            return _ownHealthSense.Health.Evaluate();
        }

        double? GetLow(double x) => Get(LowHealth, x);
        double? GetMedium(double x) => Get(MediumHealth, x);
        double? GetHigh(double x) => Get(HighHealth, x);
        
        // Show
        var plot = new Plot();

        var low = plot.AddFunction(GetLow, color: Color.Green);
        low.Label = "Low";

        var medium = plot.AddFunction(GetMedium, color: Color.Red);
        medium.Label = "Medium";

        var high = plot.AddFunction(GetHigh, color: Color.Blue);
        high.Label = "High";
        
        plot.SetAxisLimits(0, 1, -0.1, 1.1);
        
        plot.Legend();
        
        var viewer = new FormsPlotViewer(plot);
        viewer.ShowDialog();
    }
}