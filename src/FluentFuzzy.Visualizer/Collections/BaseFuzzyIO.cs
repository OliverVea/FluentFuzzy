using FluentFuzzy.Visualizer.Events;
using FuzzyLogic.Interfaces;

namespace FluentFuzzy.Visualizer.Collections;

public abstract class BaseFuzzyIO
{
    private readonly List<MemberFunction> _memberFunctions = new();
    
    private int _antecedent;
    public IReadOnlyList<MemberFunction> MemberFunctions => _memberFunctions;
    
    public readonly Event<EventArgs> MemberFunctionAdded = new();
    public readonly Event<EventArgs> MemberFunctionRemoved = new();
    
    public double Value { get; protected set; }
    public double Min { get; init;  } = 0;
    public double Max { get; init;  } = 1;
    public string Name { get; }
    
    protected BaseFuzzyIO(string name)
    {
        Name = name;
    }
    
    public void AddMemberFunction(IMemberFunction memberFunction, IHasCentroid centroid, string name, Color colorPaletteColor)
    {
        var wrapper = new MemberFunction(
            memberFunction, 
            centroid,
            name, 
            _antecedent++, 
            colorPaletteColor);
        
        _memberFunctions.Add(wrapper);
        
        AddMemberFunction(wrapper);
        
        MemberFunctionAdded.Invoke(this, EventArgs.Empty);
    }

    protected abstract void AddMemberFunction(MemberFunction wrapper);

    public void RemoveMemberFunction(string name)
    {
        var toRemove = _memberFunctions.Where(x => x.Name == name).ToArray();
        foreach (var function in toRemove)
        {
            _memberFunctions.Remove(function);
            MemberFunctionRemoved.Invoke(this, EventArgs.Empty);
        }
    }
}