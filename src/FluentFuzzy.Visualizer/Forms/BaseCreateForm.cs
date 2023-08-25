using FluentFuzzy.Visualizer.UserControls;

namespace FluentFuzzy.Visualizer.Forms;

public class BaseCreateForm<T> : Form where T : UserControl, ICreateOptions, new()
{
    private const int TitleHeight = 30;
    private const int ButtonHeight = 40;
    
    private readonly TableLayoutPanel _layout = new()
    {
        Dock = DockStyle.Fill,
        AutoSize = true,
        AutoSizeMode = AutoSizeMode.GrowAndShrink
    };

    private readonly Label _title = new()
    {
        Text = "Create Fuzzy Input",
        Font = Constants.Fonts.SecondaryTitle,
        TextAlign = ContentAlignment.MiddleCenter,
        Anchor = AnchorStyles.Left | AnchorStyles.Right,
        Height = TitleHeight
    };

    private readonly CreateCancelButtons _buttons = new()
    {
        Height = ButtonHeight,
        Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
    };

    protected readonly T Options = new()
    {
        Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
    };
    
    public BaseCreateForm()
    {
        Width = 350;
        Height = 550;
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        AutoScaleMode = AutoScaleMode.Font;
        Text = "Create Fuzzy Input";

        _buttons.CancelButton.Click += Cancel;
        _buttons.CreateButton.Click += Create;

        _layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _layout.Controls.Add(_title);
        _layout.Controls.Add(Options);
        _layout.Controls.Add(_buttons);
        
        Controls.Add(_layout);
    }
    
    private void Cancel(object? sender, EventArgs e)
    {
        _buttons.CancelButton.Click -= Cancel;
        Close();
    }
    
    protected virtual void Create(object? sender, EventArgs e)
    {
        Options.Create();
        _buttons.CreateButton.Click -= Cancel;
        Close();
    }
}