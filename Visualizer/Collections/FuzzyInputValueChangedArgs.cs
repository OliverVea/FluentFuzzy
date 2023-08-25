namespace Visualizer.Collections;

public class FuzzyInputValueChangedArgs : EventArgs
{
    public double OldValue { get; init; }
    public double NewValue { get; init; }
}