﻿using System.Collections.Generic;

namespace FluentFuzzy
{
    internal static class CentroidExtensions
    {
        internal static Centroid Sum(this IEnumerable<Centroid> centroids)
        {
            var weight = 0d;
            var value = 0d;
            
            foreach (var centroid in centroids)
            {
                weight += centroid.Weight;
                value += centroid.Value * centroid.Weight;
            }

            if (weight > 0) value /= weight;
            
            return new Centroid(value, weight);
        }
    }
}