using Visualizer.Events;

namespace Visualizer.Collections;

public static class FuzzyInputCollection
{
    private static List<FuzzyInput> _fuzzyInputs = new();
    public static IReadOnlyList<FuzzyInput> FuzzyInputs => _fuzzyInputs;
    

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
        _fuzzyInputs.Add(input);
        var args = new InputAddedArgs(input);
        InputAdded.Invoke(null, args);
    }

    public static void RemoveFuzzyInput(FuzzyInput input)
    {
        _fuzzyInputs.Add(input);
        var args = new InputRemovedArgs(input);
        InputRemoved.Invoke(null, args);
    }
}