namespace FluentFuzzy
{
    public class Centroid
    {
        public Centroid(double value, double weight)
        {
            Value = value;
            Weight = weight;
        }

        public double Value { get; }
        public double Weight { get; }

        public static readonly Centroid Zero = new(0, 0);
    }
}