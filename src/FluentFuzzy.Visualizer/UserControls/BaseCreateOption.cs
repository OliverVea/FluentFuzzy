namespace FluentFuzzy.Visualizer.UserControls;

public abstract class BaseCreateOption : UserControl, ICreateOptions
{
    public abstract void Create();

    protected static TextBox GetTextBox() => new()
    {
        Dock = DockStyle.Fill
    };
    
    protected static NumericUpDown GetNumericUpDown(decimal increment) => new()
    {
        DecimalPlaces = 2,
        Increment = increment,
        Dock = DockStyle.Fill
    };

    protected BaseCreateOption()
    {
        Height = 400;
        Controls.Add(_layout);
    }
    
    private readonly TableLayoutPanel _layout = new()
    {
        ColumnCount = 2,
        Dock = DockStyle.Fill
    };

    protected void AddOption(string text, Control control)
    {
        var label = new Label
        {
            Text = text,
            Dock = DockStyle.Fill
        };
        
        _layout.Controls.Add(label);
        _layout.Controls.Add(control);
    }

}