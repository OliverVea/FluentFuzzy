using ScottPlot;
using Visualizer.Collections;
using Cursor = System.Windows.Forms.Cursor;

namespace Visualizer.UserControls;

public class FuzzyInputDisplay : UserControl
{
    private const string ContextAddFunction = "Add Function";
    private const string ContextRemoveFunction = "Remove Function";
    private const string ContextSetValue = "Set Value";

    private readonly ContextMenuStrip _contextMenu = new()
    {
    };

    private readonly ToolStripMenuItem _addFunction = new()
    {
        Text = ContextAddFunction,
        DropDownItems = { "A" }
    };

    private readonly ToolStripMenuItem _removeFunction = new()
    {
        Text = ContextRemoveFunction
    };

    private readonly ToolStripMenuItem _setValue = new()
    {
        Text = ContextSetValue
    };
    
    private readonly FormsPlot _plot = new()
    {
        Dock = DockStyle.Fill
    };

    private readonly FuzzyInput _input;
    
    public FuzzyInputDisplay(FuzzyInput input)
    {
        _input = input;
        BorderStyle = BorderStyle.FixedSingle;

        RefreshPlot();
        
        _plot.Plot.Title(input.Name);
        _plot.Plot.SetAxisLimits(input.Min, input.Max, 0, 1.1);
        _plot.Configuration.Zoom = false;
        _plot.Configuration.Pan = false;
        _plot.Plot.XLabel("Value");
        _plot.Plot.YLabel("Membership");
        _plot.Refresh();

        _plot.RightClicked += OnPlotRightClicked;

        _addFunction.Click += OnAddFunctionClicked;
        _contextMenu.Items.Add(_addFunction);

        Controls.Add(_plot);

        Height = 250;
    }

    private void OnAddFunctionClicked(object? sender, EventArgs eventArgs)
    {
    }

    private void OnPlotRightClicked(object? sender, EventArgs e)
    {
        var position = _plot.PointToClient(Cursor.Position);
        _contextMenu.Show(_plot, position);
    }

    private void RefreshPlot()
    {
        _plot.Plot.Clear();

        foreach (var membershipFunction in _input.MemberFunctions)
        {
            var f = _plot.Plot.AddFunction(x => membershipFunction.Function.Evaluate(x));
            f.Label = membershipFunction.Name;
            f.FillType = FillType.FillAbove;
        }

        _plot.Plot.Legend();
    }

    /*
    private void OnSliderChanged(object? sender, EventArgs e)
    {
        if (sender is not TrackBar trackBar) return;

        var value = (double)trackBar.Value / trackBar.Maximum;
        
        _input.SetValue(value);
    }
    */

    /*
    private readonly TrackBar _slider = new()
    {
        Dock = DockStyle.Bottom,
        Minimum = 0,
        Maximum = 100
    };
    */
}