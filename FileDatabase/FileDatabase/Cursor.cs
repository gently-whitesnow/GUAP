namespace FileDatabase;

public static class Cursor
{
    
    public static int Position = 0;

    public static (int Min, int Max) ViewRange = (0, 0);

    private const int MinBorder = 0;
    public static int MaxBorder;

    public static void Init(int maxValues)
    {
        Position = 0;
        MaxBorder = maxValues;
        ViewRange.Min = 0;
        ViewRange.Max = 7 <= maxValues ? 7 : maxValues;
    }

    public static void Up()
    {
        if (Position <= MinBorder) return;

        Position--;
        if (Position < ViewRange.Min)
        {
            ViewRange.Max--;
            ViewRange.Min--;
        }
    }

    public static void Down()
    {
        if (Position >= MaxBorder) return;

        Position++;
        if (Position > ViewRange.Max)
        {
            ViewRange.Max++;
            ViewRange.Min++;
        }
    }
}