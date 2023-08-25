using Visualizer.UserControls;

namespace Visualizer.Forms;

internal sealed class MainForm : Form
{
    private const int DefaultWidth = 800;
    private const int DefaultHeight = 600;
    private const int DefaultRuleHeight = 150;
    
    private readonly InputOutputContainer _inputOutputContainer = new()
    {
        Dock = DockStyle.Fill
    };

    private readonly RuleOverview _ruleOverview = new()
    {
        Dock = DockStyle.Fill,
        BorderStyle = BorderStyle.FixedSingle
    };

    private readonly SplitContainer _splitContainer = new()
    {
        Dock = DockStyle.Fill,
        SplitterDistance = DefaultHeight - DefaultRuleHeight,
        Orientation = Orientation.Horizontal
    };

    public MainForm()
    {
        Size = new Size(DefaultWidth, DefaultHeight);
        AutoScaleMode = AutoScaleMode.Font;
        Text = "FluentFuzzy Visualizer";
        Icon = new Icon("./icon.ico", 32, 32);

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _splitContainer.Panel1.Controls.Add(_inputOutputContainer);
        _splitContainer.Panel2.Controls.Add(_ruleOverview);
        
        Controls.Add(_splitContainer);
    }
}