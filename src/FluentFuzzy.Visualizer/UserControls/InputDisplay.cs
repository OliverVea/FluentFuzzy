using System.ComponentModel;
using FluentFuzzy.Visualizer.Forms;
using ScottPlot;
using Cursor = System.Windows.Forms.Cursor;

namespace FluentFuzzy.Visualizer.UserControls;

public class InputDisplay : UserControl
{
    private const string ContextAddFunction = "Add Function";
    private const string ContextRemoveFunction = "Remove Function";
    private const string ContextSetValue = "Set Value";
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

    private readonly ToolStripMenuItem _setValue = new()
    {
        Text = ContextSetValue
    };

    private readonly ToolStripMenuItem _saveImage = new()
    {
        Text = ContextSaveImage
    };
    
    private readonly FormsPlot _plot = new()
    {
        Dock = DockStyle.Fill
    };

    private readonly Collections.FuzzyInput _input;
    
    public InputDisplay(Collections.FuzzyInput input)
    {
        _input = input;
        
        _functionOptions = new Dictionary<string, Action>
        {
            { "Triangle", () => new CreateTriangleForm(_input).Show() }
        };
        
        _plot.Configuration.Zoom = false;
        _plot.Configuration.Pan = false;
        _plot.Plot.XLabel(input.Name);
        _plot.Plot.YLabel("Membership");
        _plot.Refresh();
        
        var items = _functionOptions.Keys.Select(x => new ToolStripMenuItem(x)).OfType<ToolStripItem>().ToArray();
        _addFunction.DropDownItems.AddRange(items);
        _contextMenu.Items.Add(_setValue);
        _contextMenu.Items.Add(new ToolStripSeparator());
        _contextMenu.Items.Add(_addFunction);
        _contextMenu.Items.Add(_removeFunction);
        _contextMenu.Items.Add(new ToolStripSeparator());
        _contextMenu.Items.Add(_saveImage);

        Controls.Add(_plot);

        Height = 250;

        RefreshPlot();
        
        _input.FuzzyInputValueChanged.EventHandler += OnFizzyInputValueChanged;
        _plot.RightClicked += OnPlotRightClicked;
        _addFunction.DropDownItemClicked += OnAddFunctionClicked;
        _removeFunction.DropDownItemClicked += OnRemoveFunctionClicked;
        _setValue.Click += OnSetValueClicked;
        _saveImage.Click += OnSaveImageClicked;
        _input.MemberFunctionAdded.EventHandler += OnInputMemberFunctionAdded;
        _input.MemberFunctionRemoved.EventHandler += OnInputMemberFunctionRemoved;
    }

    private void OnSaveImageClicked(object? sender, EventArgs e)
    {
        var fileDialog = new SaveFileDialog
        {
            FileName = _input.Name,
            DefaultExt = "png",
            AddExtension = true,
            Filter = "PNG files|*.png"
        };
        
        fileDialog.FileOk += (s, _) =>
        {
            if (s is not SaveFileDialog saveFileDialog) return;
            if (saveFileDialog.FileName is not { } fileName) return;

            _plot.Plot.SaveFig(fileName, scale: 4);
        };
        
        fileDialog.ShowDialog();
    }

    private void OnNumberDialogClosing(object? sender, CancelEventArgs e)
    {
        if (sender is not NumberDialog numberDialog) return;
        _input.SetValue(numberDialog.Value);
    }

    private void OnFizzyInputValueChanged(object? sender, EventArgs e)
    {
        RefreshPlot();
    }

    private void OnSetValueClicked(object? sender, EventArgs eventArgs)
    {
        var numberDialog = new NumberDialog(_input.Value);
        numberDialog.Closing += OnNumberDialogClosing;
        numberDialog.Show();
    }

    private void OnRemoveFunctionClicked(object? sender, ToolStripItemClickedEventArgs e)
    {
        _input.RemoveMemberFunction(e.ClickedItem.Text);
    }

    private void OnInputMemberFunctionRemoved(object? sender, EventArgs e)
    {
        RefreshPlot();
        RefreshRemoveDropDown();
    }

    private void OnInputMemberFunctionAdded(object? sender, EventArgs e)
    {
        RefreshPlot();
        RefreshRemoveDropDown();
    }

    private void RefreshRemoveDropDown()
    {
        _removeFunction.DropDownItems.Clear();
        
        var items = _input.MemberFunctions.Select(x => new ToolStripMenuItem(x.Name)).OfType<ToolStripItem>().ToArray();

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

        var x = _input.Value;
        var valueLine = _plot.Plot.AddVerticalLine(x);
        valueLine.Color = Color.Black;
        valueLine.LineStyle = LineStyle.Dash;
        valueLine.LineWidth = 1;

        foreach (var membershipFunction in _input.MemberFunctions)
        {
            var f = _plot.Plot.AddFunction(v => membershipFunction.Function.Evaluate(v));
            f.Color = membershipFunction.Color;

            var y = membershipFunction.Function.Evaluate(x);
            var yText = $"{y:N}";
            
            f.Label = $"{membershipFunction.Name} ({yText})";
            
            if (y <= 0) continue;
            
            var p = _plot.Plot.AddPoint(x, y, membershipFunction.Color);
            p.Text = yText;
        }
        

        _plot.Plot.Legend();
        _plot.Plot.SetAxisLimits(_input.Min, _input.Max, 0, 1.1);
        _plot.Refresh();
    }
}