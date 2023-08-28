using System;
using System.Linq;
using FuzzyLogic.Interfaces;
using UnityEngine;

namespace FuzzyLogic.Unity
{
    [Serializable]
    public class Curve : IMemberFunction, IHasCentroid
    {
        [SerializeField] [Min(0)] private int _centroidSamples = 100;
        [SerializeField] private AnimationCurve _animationCurve = new();
        
        public double Evaluate(double x)
        {
            return _animationCurve.Evaluate((float)x);
        }

        public Centroid GetCentroid(double yMax)
        {
            if (yMax == 0) return Centroid.Zero;
            
            var min = _animationCurve.keys.Min(x => x.time);
            var max = _animationCurve.keys.Max(x => x.time);

            var xCentroid = 0f;
            var wCentroid = 0f;

            for (var i = 0f; i < _centroidSamples; i++)
            {
                var x = Mathf.Lerp(min, max, i / (_centroidSamples - 1));
                var y = _animationCurve.Evaluate(x);

                y = Mathf.Min(y, (float)yMax);
                
                xCentroid += x * y;
                wCentroid += y;
            }
            
            xCentroid /= wCentroid * _centroidSamples;
            wCentroid /= _centroidSamples;
            
            return new Centroid(xCentroid, wCentroid);
        }
    }
}