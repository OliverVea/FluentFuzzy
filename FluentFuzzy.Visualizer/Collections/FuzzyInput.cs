using FluentFuzzy.Visualizer.Events;
using FuzzyLogic.Interfaces;
using FuzzyLogic.MemberFunctions;

namespace FluentFuzzy.Visualizer.Collections;

public class FuzzyInput
{
    private int _antecedent;
    private readonly FuzzyLogic.FuzzyInput _input;
    private readonly List<MemberFunction> _memberFunctions = new();
    public IReadOnlyList<MemberFunction> MemberFunctions => _memberFunctions;
    
    public readonly Event<FuzzyInputValueChangedArgs> FuzzyInputValueChanged = new();
    public readonly Event<EventArgs> MemberFunctionAdded = new();
    
    public double Value { get; private set; }
    public double Min { get; init;  } = 0;
    public double Max { get; init;  } = 1;
    public string Name { get; }

    
    public FuzzyInput(string name)
    {
        Name = name;
        _input = new FuzzyLogic.FuzzyInput(() => Value);

        AddMemberFunction(new Triangle(0.25, 0.5, 0.75), "Medium");
    }

    public void SetValue(double value)
    {
        var args = new FuzzyInputValueChangedArgs { OldValue = Value, NewValue = value };
        Value = value;
        FuzzyInputValueChanged.Invoke(this, args);
    }

    public void AddMemberFunction(IMemberFunction memberFunction, string name)
    {
        var wrapper = new MemberFunction
        {
            Function = memberFunction,
            Name = name,
            Antecedent = _antecedent++
        };
        _memberFunctions.Add(wrapper);
        _input.Set(wrapper.Antecedent, wrapper.Function);
        MemberFunctionAdded.Invoke(this, EventArgs.Empty);
    }
}