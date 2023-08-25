using FluentFuzzy.Visualizer.Collections;
using FluentFuzzy.Visualizer.UserControls;

namespace FluentFuzzy.Visualizer.Forms;

public class CreateTriangleForm : BaseCreateForm<CreateTriangleOptions>
{
    private readonly FuzzyInput _input;
    
    public CreateTriangleForm(FuzzyInput input)
    {
        _input = input;
    }

    protected override void Create(object? sender, EventArgs e)
    {
        Options.Create(_input);
        base.Create(sender, e);
    }
}