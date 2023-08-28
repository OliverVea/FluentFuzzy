using FluentFuzzy.Visualizer.Events;

namespace FluentFuzzy.Visualizer.Collections;

public class FuzzyOutputCollection
{
    private static readonly List<FuzzyOutput> Outputs = new();
    public static IReadOnlyList<FuzzyOutput> FuzzyOutputs => Outputs;
    

    public static readonly Event<OutputAddedArgs> OutputAdded = new();
    public class OutputAddedArgs : EventArgs
    {
        public FuzzyOutput AddedOutput { get; private set; }
        public OutputAddedArgs(FuzzyOutput output)
        {
            AddedOutput = output;
        }
    }
    

    public static readonly Event<OutputRemovedArgs> OutputRemoved = new();
    public class OutputRemovedArgs : EventArgs
    {
        public FuzzyOutput RemovedOutput { get; private set; }
        public OutputRemovedArgs(FuzzyOutput output)
        {
            RemovedOutput = output;
        }
    }
    

    public static void AddFuzzyOutput(FuzzyOutput output)
    {
        Outputs.Add(output);
        var args = new OutputAddedArgs(output);
        OutputAdded.Invoke(null, args);
    }

    public static void RemoveFuzzyOutput(FuzzyOutput output)
    {
        Outputs.Add(output);
        var args = new OutputRemovedArgs(output);
        OutputRemoved.Invoke(null, args);
    }
}