using FluentFuzzy.Visualizer.Events;

namespace FluentFuzzy.Visualizer.Collections;

public static class FuzzyInputCollection
{
    private static readonly List<FuzzyInput> Inputs = new();
    public static IReadOnlyList<FuzzyInput> FuzzyInputs => Inputs;
    

    public static readonly Event<InputAddedArgs> InputAdded = new();
    public class InputAddedArgs : EventArgs
    {
        public FuzzyInput AddedInput { get; private set; }
        public InputAddedArgs(FuzzyInput input)
        {
            AddedInput = input;
        }
    }
    

    public static readonly Event<InputRemovedArgs> InputRemoved = new();
    public class InputRemovedArgs : EventArgs
    {
        public FuzzyInput RemovedInput { get; private set; }
        public InputRemovedArgs(FuzzyInput input)
        {
            RemovedInput = input;
        }
    }
    

    public static void AddFuzzyInput(FuzzyInput input)
    {
        Inputs.Add(input);
        var args = new InputAddedArgs(input);
        InputAdded.Invoke(null, args);
    }

    public static void RemoveFuzzyInput(FuzzyInput input)
    {
        Inputs.Add(input);
        var args = new InputRemovedArgs(input);
        InputRemoved.Invoke(null, args);
    }
}