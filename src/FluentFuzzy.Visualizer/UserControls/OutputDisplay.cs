using FluentFuzzy.Visualizer.Forms;
using ScottPlot;
using Cursor = System.Windows.Forms.Cursor;

namespace FluentFuzzy.Visualizer.UserControls;

public class OutputDisplay : UserControl
{
    private const string ContextAddFunction = "Add Function";
    private const string ContextRemoveFunction = "Remove Function";
    private const string ContextSaveImage = "Save Image";

    private readonly ContextMenuStrip _contextMenu = new();

    private readonly IReadOnlyDictionary<string, Action> _functionOptions;

    private readonly ToolStripMenuItem _addFunction = new()
    {
        Text = ContextAddFunction
    };

    private readonly ToolStripMenuItem _removeFunction = new()
    {
        Text = ContextRemoveFunction
    };

    private readonly ToolStripMenuItem _saveImage = new()
    {
        Text = ContextSaveImage
    };
    
    private readonly FormsPlot _plot = new()
    {
        Dock = DockStyle.Fill
    };

    private readonly Collections.FuzzyOutput _output;
    
    public OutputDisplay(Collections.FuzzyOutput output)
    {
        _output = output;
        
        _functionOptions = new Dictionary<string, Action>
        {
            { "Triangle", () => new CreateTriangleForm(_output).Show() }
        };
        
        _plot.Configuration.Zoom = false;
        _plot.Configuration.Pan = false;
        _plot.Plot.XLabel(output.Name);
        _plot.Plot.YLabel("Membership");
        _plot.Refresh();
        
        var items = _functionOptions.Keys.Select(x => new ToolStripMenuItem(x)).OfType<ToolStripItem>().ToArray();
        _addFunction.DropDownItems.AddRange(items);
        _contextMenu.Items.Add(_addFunction);
        _contextMenu.Items.Add(_removeFunction);
        _contextMenu.Items.Add(new ToolStripSeparator());
        _contextMenu.Items.Add(_saveImage);

        Controls.Add(_plot);

        Height = 250;

        RefreshPlot();
        
        _plot.RightClicked += OnPlotRightClicked;
        _addFunction.DropDownItemClicked += OnAddFunctionClicked;
        _removeFunction.DropDownItemClicked += OnRemoveFunctionClicked;
        _saveImage.Click += OnSaveImageClicked;
        _output.MemberFunctionAdded.EventHandler += OnOutputMemberFunctionAdded;
        _output.MemberFunctionRemoved.EventHandler += OnOutputMemberFunctionRemoved;
    }

    private void OnSaveImageClicked(object? sender, EventArgs e)
    {
        var fileDialog = new SaveFileDialog
        {
            FileName = _output.Name,
            DefaultExt = "png",
            AddExtension = true,
            Filter = "PNG files|*.png"
        };
        
        fileDialog.FileOk += (s, args) =>
        {
            if (s is not SaveFileDialog saveFileDialog) return;
            if (saveFileDialog.FileName is not { } fileName) return;

            _plot.Plot.SaveFig(fileName, scale: 4);
        };
        
        fileDialog.ShowDialog();
    }

    private void OnRemoveFunctionClicked(object? sender, ToolStripItemClickedEventArgs e)
    {
        _output.RemoveMemberFunction(e.ClickedItem.Text);
    }

    private void OnOutputMemberFunctionRemoved(object? sender, EventArgs e)
    {
        RefreshPlot();
        RefreshRemoveDropDown();
    }

    private void OnOutputMemberFunctionAdded(object? sender, EventArgs e)
    {
        RefreshPlot();
        RefreshRemoveDropDown();
    }

    private void RefreshRemoveDropDown()
    {
        _removeFunction.DropDownItems.Clear();
        
        var items = _output.MemberFunctions.Select(x => new ToolStripMenuItem(x.Name)).OfType<ToolStripItem>().ToArray();

        _removeFunction.Enabled = items.Any();
        _removeFunction.DropDownItems.AddRange(items);
    }

    private void OnAddFunctionClicked(object? sender, ToolStripItemClickedEventArgs toolStripItemClickedEventArgs)
    {
        var key = toolStripItemClickedEventArgs.ClickedItem.Text;
        _functionOptions[key]();
    }

    private void OnPlotRightClicked(object? sender, EventArgs e)
    {
        var position = _plot.PointToClient(Cursor.Position);
        _contextMenu.Show(_plot, position);
    }

    private void RefreshPlot()
    {
        _plot.Plot.Clear();

        var x = _output.Value;
        var valueLine = _plot.Plot.AddVerticalLine(x);
        valueLine.Color = Color.Black;
        valueLine.LineStyle = LineStyle.Dash;
        valueLine.LineWidth = 1;

        var centroidSum = _output.MemberFunctions.Sum(f => f.Centroid.GetCentroid(1).Weight);
        
        foreach (var membershipFunction in _output.MemberFunctions)
        {
            var f = _plot.Plot.AddFunction(x => membershipFunction.Function.Evaluate(x));
            f.Color = membershipFunction.Color;

            var c = membershipFunction.Centroid.GetCentroid(1);
            var size = (float)(c.Weight / centroidSum * 25f); 
            _plot.Plot.AddPoint(c.Value, c.Weight, shape: MarkerShape.cross, color: membershipFunction.Color, size: size);

            var y = membershipFunction.Function.Evaluate(x);
            var yText = $"{y:N}";
            
            f.Label = $"{membershipFunction.Name} ({yText})";
            
            if (y <= 0) continue;
            
            var p = _plot.Plot.AddPoint(x, y, membershipFunction.Color);
            p.Text = yText;
        }
        

        _plot.Plot.Legend();
        _plot.Plot.SetAxisLimits(_output.Min, _output.Max, 0, 1.1);
        _plot.Refresh();
    }
}