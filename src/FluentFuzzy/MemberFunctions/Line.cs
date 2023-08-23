using FuzzyLogic.Interfaces;

namespace FuzzyLogic.MemberFunctions
{
    public class Line : IMemberFunction
    {
        private readonly double _min;
        private readonly double _max;
        
        private readonly bool _reversed;

        public Line(double min, double max)
        {
            _reversed = max < min;

            if (_reversed)
            {
                _min = max;
                _max = min;
            }
            else
            {
                _min = min;
                _max = max;
            }

        }
        
        public double Evaluate(double x)
        {
            if (x < _min) return _reversed ? 1 : 0;
            if (x > _max) return _reversed ? 0 : 1;

            var slope = 1 / (_max - _min);
            
            if (_reversed) return 1 - (x - _min) * slope;
            return (x - _min) * slope;
        }
    }
}