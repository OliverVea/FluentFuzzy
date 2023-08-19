using System;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.MemberFunctions
{
    public class Value : IMemberFunction
    {
        private readonly double _value;
        private readonly double _tolerance;

        public Value(double value, double tolerance = 0.00001d)
        {
            _value = value;
            _tolerance = tolerance;
        }

        public double Evaluate(double x)
        {
            return Math.Abs(x - _value) < _tolerance ? 1 : 0;
        }
    }
}