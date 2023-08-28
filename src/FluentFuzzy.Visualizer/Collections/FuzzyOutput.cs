namespace FluentFuzzy.Visualizer.Collections;

public class FuzzyOutput : BaseFuzzyIO
{
    private readonly FuzzyLogic.FuzzyOutput _output;
    
    public FuzzyOutput(string name) : base(name)
    {
        _output = new FuzzyLogic.FuzzyOutput();
    }

    protected override void AddMemberFunction(MemberFunction wrapper)
    {
        _output.Set(wrapper.Hash, wrapper.Centroid);
    }
}