using System.Drawing;
using FuzzyLogic.MemberFunctions;
using ScottPlot;

namespace FuzzyLogic.Visualization;

public class FuzzyInputMemberFunctionPlot
{
    private readonly FuzzyInput _fuzzyInput;
    private readonly Dictionary<int, string> _membershipLabels = new();
    private readonly Dictionary<int, Color> _membershipColors = new();
    private readonly Plot _plot = new();

    public FuzzyInputMemberFunctionPlot(FuzzyInput fuzzyInput)
    {
        _fuzzyInput = fuzzyInput;
    }
    
    public void AddLabel(int id, string label) => _membershipLabels.Add(id, label);
    public void AddColor(int id, Color color) => _membershipColors.Add(id, color);
    public void XLabel(string label) => _plot.XLabel(label);
    
    public void ShowMemberFunctions(double min, double max)
    {
        foreach (var function in _fuzzyInput.MemberFunctions) DrawMemberFunction(function);

        _plot.SetAxisLimits(min, max, -0.1, 1.1);
        _plot.Legend();

        new FormsPlotViewer(_plot).ShowDialog();
    }

    private void DrawMemberFunction(KeyValuePair<int, IMemberFunction> membershipFunction)
    {
        var (id, function) = membershipFunction;
        
        _membershipColors.TryGetValue(id, out var color);
        
        var activeLine = _plot.AddFunction(x => NonZero(function, x), color: color);

        var hasLabel = _membershipLabels.TryGetValue(id, out var label);
        if (hasLabel) activeLine.Label = label;
    }

    private static double? NonZero(IMemberFunction function, double x)
    {
        var y = function.Evaluate(x);

        if (!double.IsFinite(y)) return null;
        
        return y > 0 ? y : null;
    }
}