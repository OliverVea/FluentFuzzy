using FluentFuzzy.Interfaces;

namespace FluentFuzzy.Visualizer.Collections;

public record MemberFunction(IMemberFunction Function, IHasCentroid Centroid, string Name, int Hash, Color Color)
{
}