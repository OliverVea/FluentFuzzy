using FluentFuzzy.Visualizer.Events;

namespace FluentFuzzy.Visualizer.Collections;

public class FuzzyInput : BaseFuzzyIO
{
    private readonly FuzzyLogic.FuzzyInput _input;
    
    public readonly Event<FuzzyInputValueChangedArgs> FuzzyInputValueChanged = new();
    
    public FuzzyInput(string name) : base(name)
    {
        _input = new FuzzyLogic.FuzzyInput(() => Value);
    }
    

    public void SetValue(double value)
    {
        var args = new FuzzyInputValueChangedArgs { OldValue = Value, NewValue = value };
        Value = value;
        FuzzyInputValueChanged.Invoke(this, args);
    }

    protected override void AddMemberFunction(MemberFunction wrapper)
    {
        _input.Set(wrapper.Hash, wrapper.Function);
    }
}