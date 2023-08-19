using System;
using System.Collections.Generic;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.MemberFunctions
{
    public class Trapezoid : IMemberFunction, IHasCentroid
    {
        private readonly double _min;
        private readonly double _centerLeft;
        private readonly double _centerRight;
        private readonly double _max;
            
        public Trapezoid(double min, double centerLeft, double centerRight, double max)
        {
            _min = min;
            _centerLeft = centerLeft;
            _centerRight = centerRight;
            _max = max;
        }

        double IMemberFunction.Evaluate(double x)
        {
            if (!double.IsFinite(x)) return Double.NaN;
            
            if (x < _min || x > _max) return 0.0;
            
            if (x < _centerLeft) return (x - _min) / (_centerLeft - _min);
            if (x >= _centerLeft && x <= _centerRight) return 1.0;
            if (x > _centerRight) return (_max - x) / (_max - _centerRight);
            
            throw new InvalidOperationException();
        }

        Centroid IHasCentroid.GetCentroid(double y)
        {
            if (y == 0) return new Centroid(0, 0);
            
            var centroids = new List<Centroid>();
            
            var leftSlope = 1 / (_centerLeft - _min);
            var leftMax = y / leftSlope + _min;
            var leftCenter = (leftMax + _min) / 2;
            var leftWeight = (leftMax - _min) * y / 2;
            centroids.Add(new Centroid(leftCenter, leftWeight));

            var rightSlope = 1 / (_max - _centerRight);
            var rightMin = _max - y / rightSlope;
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