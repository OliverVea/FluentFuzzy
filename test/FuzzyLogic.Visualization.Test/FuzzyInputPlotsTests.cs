using System.Drawing;

namespace FuzzyLogic.Visualization.Test;

public partial class FuzzyInputPlotsTests
{
    [Test]
    public void MemberFunctionPlot_Health()
    {
        // Arrange
        var plot = new FuzzyInputMemberFunctionPlot(Health);

        plot.AddLabel(Critical, "Critical");
        plot.AddLabel(Low, "Low");
        plot.AddLabel(Medium, "Medium");
        plot.AddLabel(High, "High");
        plot.AddLabel(Max, "Max");
        
        plot.AddColor(Critical, Color.Red);
        plot.AddColor(Low, Color.Orange);
        plot.AddColor(Medium, Color.Yellow);
        plot.AddColor(High, Color.Green);
        plot.AddColor(Max, Color.Blue);
        
        plot.XLabel("Health [%]");

        // Act
        plot.ShowMemberFunctions(0, 1);
        
        // Wait
    }
}