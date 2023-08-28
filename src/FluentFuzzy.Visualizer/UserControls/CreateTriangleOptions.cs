using FluentFuzzy.Visualizer.Collections;
using FuzzyLogic.MemberFunctions;

namespace FluentFuzzy.Visualizer.UserControls;

public class CreateTriangleOptions : BaseCreateOption
{
    private const decimal Increment = (decimal)0.05;
    
    
    private readonly TextBox _nameTextBox = GetTextBox();
    private readonly NumericUpDown _minInput = GetNumericUpDown(Increment);
    private readonly NumericUpDown _centerInput = GetNumericUpDown(Increment);
    private readonly NumericUpDown _maxInput = GetNumericUpDown(Increment);
    
    private readonly ColorDialog _colorPalette = new();
    private readonly Panel _colorDisplay = new()
    {
        Size = new Size(20, 20),
        Anchor = AnchorStyles.Right | AnchorStyles.Top
    };
    
    public CreateTriangleOptions()
    {
        AddOption("Name", _nameTextBox);
        AddOption("Start", _minInput);
        AddOption("Center", _centerInput);
        AddOption("End", _maxInput);

        _colorDisplay.BackColor = Color.Red;
        _colorPalette.Color = _colorDisplay.BackColor;
        _colorDisplay.Click += OnColorDisplayClick;
        AddOption("Color", _colorDisplay);
    }

    private void OnColorDisplayClick(object? sender, EventArgs e)
    {
        _colorPalette.ShowDialog();
        _colorDisplay.BackColor = _colorPalette.Color;
    }

    public override void Create() { }
    public override void RegisterCreateCallback(Action<object?, EventArgs> callback)
    {
        _nameTextBox.KeyDown += (s, a) => OnInputKeyDown(s, a, callback);
        _minInput.KeyDown += (s, a) => OnInputKeyDown(s, a, callback);
        _centerInput.KeyDown += (s, a) => OnInputKeyDown(s, a, callback);
        _maxInput.KeyDown += (s, a) => OnInputKeyDown(s, a, callback);
    }

    private void OnInputKeyDown(object? sender, KeyEventArgs args, Action<object?, EventArgs> callback)
    {
        if (args.KeyCode != Keys.Enter) return;
        callback(sender, args);
    }

    public void Create(BaseFuzzyIO input)
    {
        var triangle = new Triangle(
            (double)_minInput.Value, 
            (double)_centerInput.Value, 
            (double)_maxInput.Value);

        input.AddMemberFunction(
            triangle, 
            triangle,
            _nameTextBox.Text, 
            _colorPalette.Color);
    }
}