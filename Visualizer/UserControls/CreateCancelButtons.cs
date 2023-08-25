namespace Visualizer.UserControls;

public class CreateCancelButtons : UserControl
{
    
    private readonly TableLayoutPanel _layout = new()
    {
        ColumnCount = 2,
        RowCount = 1,
        Dock = DockStyle.Fill
    };
    
    public readonly Button CancelButton = new()
    {
        Text = "Cancel",
        Dock = DockStyle.Fill
    };
    
    public readonly Button CreateButton = new()
    {
        Text = "Create",
        Dock = DockStyle.Fill
    };

    public CreateCancelButtons()
    {
        _layout.ColumnStyles.Add(Constants.ColumnStyles.FiftyPercent);
        _layout.ColumnStyles.Add(Constants.ColumnStyles.FiftyPercent);
        _layout.Controls.Add(CreateButton);
        _layout.Controls.Add(CancelButton);
        
        Controls.Add(_layout);
    }
    

}