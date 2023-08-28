using FluentFuzzy.Visualizer.Collections;

namespace FluentFuzzy.Visualizer.UserControls;

public class CreateOutputOptions : BaseCreateOption
{
    private const decimal Increment = (decimal)0.5;
    
    private readonly TextBox _nameTextBox = GetTextBox();
    private readonly NumericUpDown _minOutput = GetNumericUpDown(Increment);
    private readonly NumericUpDown _maxOutput = GetNumericUpDown(Increment);

    public CreateOutputOptions()
    {
        AddOption("Name", _nameTextBox);
        AddOption("Min", _minOutput);
        AddOption("Max", _maxOutput);
    }

    public override void Create()
    {
        var name = _nameTextBox.Text ?? "";
        var fuzzyOutput = new Collections.FuzzyOutput(name)
        {
            Min = (double)_minOutput.Value,
            Max = (double)_maxOutput.Value,
        };
        
        FuzzyOutputCollection.AddFuzzyOutput(fuzzyOutput);
    }

    public override void RegisterCreateCallback(Action<object?, EventArgs> callback)
    {
        _nameTextBox.KeyDown += (s, a) => OnOutputKeyDown(s, a, callback);
        _minOutput.KeyDown += (s, a) => OnOutputKeyDown(s, a, callback);
        _maxOutput.KeyDown += (s, a) => OnOutputKeyDown(s, a, callback);
    }

    private void OnOutputKeyDown(object? sender, KeyEventArgs args, Action<object?, EventArgs> callback)
    {
        if (args.KeyCode != Keys.Enter) return;
        callback(sender, args);
    }
}