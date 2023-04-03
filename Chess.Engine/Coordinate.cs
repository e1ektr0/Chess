namespace Chess.Engine;

public class Coordinate
{
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; } //symbol
    public int Y { get; set; } //number


    public static bool IsValid(int x, int y)
    {
        return x >= 0 && y >= 0 && x < 8 && y < 8;
    }


    public bool IsValid()
    {
        return IsValid(X, Y);
    }
}