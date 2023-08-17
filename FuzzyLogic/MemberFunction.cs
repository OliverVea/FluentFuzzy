using System;

namespace FuzzyLogic.Test
{
    public abstract class MemberFunction
    {
        private MemberFunction()
        {
        }

        public abstract double Evaluate(double value);

        public class Triangle : MemberFunction
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

            public override double Evaluate(double value)
            {
                if (value < _min || value > _max) return 0.0;
            
                if (value < _center) return (value - _min) / (_center - _min);
                if (value >= _center) return (_max - value) / (_max - _center);
            
                throw new InvalidOperationException();
            }
        }

        public class Trapezoid : MemberFunction
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

            public override double Evaluate(double value)
            {
                if (value < _min || value > _max) return 0.0;
            
                if (value < _centerLeft) return (value - _min) / (_centerLeft - _min);
                if (value >= _centerLeft && value <= _centerRight) return 1.0;
                if (value > _centerRight) return (_max - value) / (_max - _centerRight);
            
                throw new InvalidOperationException();
            }
        }
    }
}

