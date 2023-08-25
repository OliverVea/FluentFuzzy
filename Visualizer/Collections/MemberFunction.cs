using FuzzyLogic.Interfaces;

namespace Visualizer.Collections;

public class MemberFunction
{
    public IMemberFunction Function { get; init; }
    public string Name { get; init; }
    public int Antecedent { get; init; }
    public Color Color { get; set; }

}