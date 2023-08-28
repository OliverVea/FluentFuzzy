namespace FluentFuzzy.Visualizer.Collections;

public class FuzzyOutput : BaseFuzzyIO
{
    private readonly FluentFuzzy.FuzzyOutput _output;
    
    public FuzzyOutput(string name) : base(name)
    {
        _output = new FluentFuzzy.FuzzyOutput();
    }

    protected override void AddMemberFunction(MemberFunction wrapper)
    {
        _output.Set(wrapper.Hash, wrapper.Centroid);
    }
}