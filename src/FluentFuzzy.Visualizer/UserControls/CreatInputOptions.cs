using FluentFuzzy.Visualizer.Collections;

namespace FluentFuzzy.Visualizer.UserControls;

public class CreatInputOptions : BaseCreateOption
{
    private readonly TextBox _nameTextBox = new();
    
    public CreatInputOptions()
    {
        AddOption("Name", _nameTextBox);
    }

    public override void Create()
    {
        var name = _nameTextBox.Text ?? "";
        var fuzzyInput = new FuzzyInput(name);
        
        FuzzyInputCollection.AddFuzzyInput(fuzzyInput);
    }
}