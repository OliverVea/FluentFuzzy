using FluentFuzzy.Visualizer.Collections;
using FluentFuzzy.Visualizer.UserControls;

namespace FluentFuzzy.Visualizer.Forms;

public class CreateTriangleForm : BaseCreateForm<CreateTriangleOptions>
{
    private readonly BaseFuzzyIO _input;
    
    public CreateTriangleForm(BaseFuzzyIO input) : base("Create Triangle")
    {
        _input = input;
    }

    protected override void Create(object? sender, EventArgs e)
    {
        Options.Create(_input);
        base.Create(sender, e);
    }
}