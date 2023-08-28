namespace FluentFuzzy.Visualizer.UserControls;

internal class InputOutputContainer : UserControl
{
    private readonly TableLayoutPanel _layout = new()
    {
        ColumnCount = 2,
        RowCount = 1,
        Dock = DockStyle.Fill,
        ColumnStyles =
        {
            Constants.ColumnStyles.FiftyPercent,
            Constants.ColumnStyles.FiftyPercent
        }
    };
    
    private readonly InputOverview _inputOverview = new()
    {
        Dock = DockStyle.Fill,
        BorderStyle = BorderStyle.FixedSingle
    };

    private readonly OutputOverview _outputOverview = new()
    {
        Dock = DockStyle.Fill,
        BorderStyle = BorderStyle.FixedSingle
    };
    
    public InputOutputContainer()
    {
        _layout.Controls.Add(_inputOverview);
        _layout.Controls.Add(_outputOverview);
        
        Controls.Add(_layout);
    }
}