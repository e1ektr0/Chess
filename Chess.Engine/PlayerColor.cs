namespace Chess.Engine;

public enum PlayerColor
{
    White = 0,
    Black = 1
}

public static class PlayerColorExtension
{
    public static PlayerColor Invert(this PlayerColor color)
    {
        return (PlayerColor)(((int)color + 1) % 2);
    } 
}