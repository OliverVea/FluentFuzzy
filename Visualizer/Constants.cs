namespace Visualizer;

public static class Constants
{
    public static class Fonts
    {
        public static Font Title => new("Arial", 16, FontStyle.Regular);
        public static Font SecondaryTitle => new("Arial", 12, FontStyle.Regular);
    }

    public static class ColumnStyles
    {
        public static ColumnStyle FiftyPercent => new(SizeType.Percent, 50);
        public static ColumnStyle AutoSize => new(SizeType.AutoSize);
    }

    public static class RowStyles
    {
        public static RowStyle FiftyPercent => new(SizeType.Percent, 50);
        public static RowStyle AutoSize => new(SizeType.AutoSize);
    }
}