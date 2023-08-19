using System;
using System.Collections.Generic;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.MemberFunctions
{
    public class Triangle : IMemberFunction, IHasCentroid
    {
        private readonly double _min;
        private readonly double _center;
        private readonly double _max;

        public Triangle(double min, double center, double max)
        {
            _min = min;
            _center = center;
            _max = max;
        }

        double IMemberFunction.Evaluate(double x)
        {
            if (!double.IsFinite(x)) return double.NaN;

            if (x < _min || x > _max) return 0.0;
            
            if (x < _center) return (x - _min) / (_center - _min);
            if (x >= _center) return (_max - x) / (_max - _center);
            
            throw new InvalidOperationException();
        }

        Centroid IHasCentroid.GetCentroid(double y)
        {
            if (y == 0) return new Centroid(0, 0);
            
            var centroids = new List<Centroid>();
            
            var leftSlope = 1 / (_center - _min);
            var leftMax = y / leftSlope;
            var leftCenter = (leftMax + _min) / 2;
            var leftWeight = (leftMax - _min) * y / 2;
            centroids.Add(new Centroid(leftCenter, leftWeight));

            var rightSlope = 1 / (_max - _center);
            var rightMin = y / rightSlope;
            var rightCenter = (_max + rightMin) / 2;
            var rightWeight = (_max - rightMin) * y / 2;
            centroids.Add(new Centroid(rightCenter, rightWeight));

            var centerCenter = (rightMin + leftMax) / 2;
            var centerWeight = (rightMin - leftMax) * y;
            centroids.Add(new Centroid(centerCenter, centerWeight));
            
            return centroids.Sum();
        }
    }
}