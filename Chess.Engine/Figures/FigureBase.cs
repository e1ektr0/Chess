namespace Chess.Engine.Figures;

public abstract class FigureBase
{
    public int X { get; set; } //symbol
    public int Y { get; set; } //number
    public PlayerColor Color { get; set; }
    public Figure Type { get; set; }

    public static FigureBase Create(Figure figure, PlayerColor color, int x, int y)
    {
        var result = Create(figure);
        result.Color = color;
        result.X = x;
        result.Y = y;
        result.Type = figure;

        return result;
    }

    private static FigureBase Create(Figure figure)
    {
        switch (figure)
        {
            case Figure.Pawn:
                return new Pawn();
            case Figure.Knight:
                return new Knight();
            case Figure.Bishop:
                return new Bishop();
            case Figure.Rook:
                return new Rook();
            case Figure.Queen:
                return new Queen();
            case Figure.King:
                return new King();
            default:
                throw new ArgumentOutOfRangeException(nameof(figure), figure, null);
        }
    }

    public abstract List<Coordinate> GetPossibleMovements(Board board);

    public virtual void Move(int x1, int y1)
    {
        X = x1;
        Y = y1;
    }

    internal static bool CheckBarrier(FigureBase? targetFigure, FigureBase currentFigure,
        List<Coordinate> result, Coordinate coordinate)
    {
        if (targetFigure != null)
        {
            if (targetFigure.Color == currentFigure.Color)
                return true;
            result.Add(coordinate);
            return true;
        }

        result.Add(coordinate);
        return false;
    }

    internal static bool CreateMovement(Board board, FigureBase currentFigure, int x, int y, List<Coordinate> result)
    {
        var coordinate = new Coordinate(x, y);
        var figure = board.Get(coordinate);
        if (CheckBarrier(figure, currentFigure, result, coordinate))
            return true;
        return false;
    }

    internal bool CheckPossibleMovement(Board board, FigureBase currentFigure, Coordinate coordinate)
    {
        if (!coordinate.IsValid())
            return false;

        var figure = board.Get(coordinate);
        if (figure == null)
            return true;

        if (figure.Color == currentFigure.Color)
            return false;
        return true;
    }

    internal void AddPossibleMovement(Board board, FigureBase currentFigure, List<Coordinate> result, int x, int y)
    {
        var coordinate = new Coordinate(x, y);
        if (CheckPossibleMovement(board, currentFigure, coordinate))
            result.Add(coordinate);
    }

    public FigureBase Copy()
    {
        return (FigureBase)MemberwiseClone();
    }
}