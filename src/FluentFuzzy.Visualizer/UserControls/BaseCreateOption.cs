namespace FluentFuzzy.Visualizer.UserControls;

public abstract class BaseCreateOption : UserControl, ICreateOptions
{
    private const int InputFieldWidth = 200;
    public abstract void Create();
    public abstract void RegisterCreateCallback(Action<object?, EventArgs> callback);

    protected static TextBox GetTextBox() => new()
    {
        AcceptsReturn = true,
        Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right,
        Width = InputFieldWidth,
    };
    
    protected static NumericUpDown GetNumericUpDown(decimal increment) => new()
    {
        DecimalPlaces = 2,
        Increment = increment,
        Minimum = decimal.MinValue,
        Maximum = decimal.MaxValue,
        Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right,
        Width = InputFieldWidth,
    };

    protected BaseCreateOption()
    {
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Controls.Add(_layout);
    }
    
    private readonly TableLayoutPanel _layout = new()
    {
        ColumnCount = 2,
        AutoSize = true,
        AutoSizeMode = AutoSizeMode.GrowAndShrink,
        ColumnStyles =
        {
            new ColumnStyle(SizeType.AutoSize),
            new ColumnStyle(SizeType.Absolute, InputFieldWidth)
        }
    };

    protected void AddOption(string text, Control control)
    {
        var label = new Label
        {
            Text = text,
            Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left
        };
        
        _layout.Controls.Add(label);
        _layout.Controls.Add(control);
    }

}