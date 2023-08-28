using FluentFuzzy.Visualizer.UserControls;

namespace FluentFuzzy.Visualizer.Forms;

public class BaseCreateForm<T> : Form where T : UserControl, ICreateOptions, new()
{
    protected readonly T Options = new();
    
    public BaseCreateForm(string title)
    {
        Text = title;
        
        MinimumSize = new Size(0, 0);
        MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        AutoScaleMode = AutoScaleMode.Font;

        Options.RegisterCreateCallback(Create);
        
        Controls.Add(Options);
    }

    protected virtual void Create(object? sender, EventArgs e)
    {
        Options.Create();
        Close();
    }
}