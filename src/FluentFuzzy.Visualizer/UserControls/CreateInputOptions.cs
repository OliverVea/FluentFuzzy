using FluentFuzzy.Visualizer.Collections;

namespace FluentFuzzy.Visualizer.UserControls;

public class CreateInputOptions : BaseCreateOption
{
    private const decimal Increment = (decimal)0.5;
    
    private readonly TextBox _nameTextBox = GetTextBox();
    private readonly NumericUpDown _minInput = GetNumericUpDown(Increment);
    private readonly NumericUpDown _maxInput = GetNumericUpDown(Increment);

    public CreateInputOptions()
    {
        AddOption("Name", _nameTextBox);
        AddOption("Min", _minInput);
        AddOption("Max", _maxInput);
    }

    public override void Create()
    {
        var name = _nameTextBox.Text ?? "";
        var fuzzyInput = new Collections.FuzzyInput(name)
        {
            Min = (double)_minInput.Value,
            Max = (double)_maxInput.Value,
        };
        
        FuzzyInputCollection.AddFuzzyInput(fuzzyInput);
    }

    public override void RegisterCreateCallback(Action<object?, EventArgs> callback)
    {
        _nameTextBox.KeyDown += (s, a) => OnInputKeyDown(s, a, callback);
        _minInput.KeyDown += (s, a) => OnInputKeyDown(s, a, callback);
        _maxInput.KeyDown += (s, a) => OnInputKeyDown(s, a, callback);
    }

    private void OnInputKeyDown(object? sender, KeyEventArgs args, Action<object?, EventArgs> callback)
    {
        if (args.KeyCode != Keys.Enter) return;
        callback(sender, args);
    }
}