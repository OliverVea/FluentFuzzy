using Visualizer.Collections;

namespace Visualizer.UserControls;

public class CreatInputOptions : UserControl, ICreateOptions
{
    private readonly TableLayoutPanel _layout = new()
    {
        ColumnCount = 2,
        Dock = DockStyle.Fill
    };

    private readonly Label _nameLabel = new()
    {
        Text = "Name",
        Dock = DockStyle.Fill
    };

    private readonly TextBox _nameTextBox = new()
    {
        Dock = DockStyle.Fill
    };

    public CreatInputOptions()
    {
        _layout.Controls.Add(_nameLabel);
        _layout.Controls.Add(_nameTextBox);
            
        Controls.Add(_layout);
    }

    public void Create()
    {
        var name = _nameTextBox.Text ?? "";
        var fuzzyInput = new FuzzyInput(name);
        
        FuzzyInputCollection.AddFuzzyInput(fuzzyInput);
    }
}