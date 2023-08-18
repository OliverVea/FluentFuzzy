namespace FuzzyLogic
{
    public class Centroid
    {
        internal Centroid(double value, double weight)
        {
            Value = value;
            Weight = weight;
        }
        
        public double Value { get; }
        public double Weight { get; }
    }
}