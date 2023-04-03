namespace Chess.Engine.Figures;

public class Rook : FigureBase
{
    public override List<Coordinate> GetPossibleMovements(Board board)
    {
        return MovementBehaviour(board, X, Y, this);
    }

    public static List<Coordinate> MovementBehaviour(Board board, int startX, int startY, FigureBase currentFigure)
    {
        var result = new List<Coordinate>();
        for (int x = startX + 1, y = startY; Coordinate.IsValid(x, y); x++)
            if (CreateMovement(board, currentFigure, x, y, result))
                break;

        for (int x = startX, y = startY + 1; Coordinate.IsValid(x, y); y++)
            if (CreateMovement(board, currentFigure, x, y, result))
                break;

        for (int x = startX - 1, y = startY; Coordinate.IsValid(x, y); x--)
            if (CreateMovement(board, currentFigure, x, y, result))
                break;

        for (int x = startX, y = startY - 1; Coordinate.IsValid(x, y); y--)
            if (CreateMovement(board, currentFigure, x, y, result))
                break;

        return result;
    }
}