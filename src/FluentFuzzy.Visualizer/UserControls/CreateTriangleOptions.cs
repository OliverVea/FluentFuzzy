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

    public void Create(FuzzyInput input)
    {
        var triangle = new Triangle(
            (double)_minInput.Value, 
            (double)_centerInput.Value, 
            (double)_maxInput.Value);

        input.AddMemberFunction(
            triangle, 
            _nameTextBox.Text, 
            _colorPalette.Color);
    }
}