namespace Chess.Engine.Figures;

public class Bishop : FigureBase
{
    public override List<Coordinate> GetPossibleMovements(Board board)
    {
        var startX = X;
        var startY = Y;

        var result = MovementBehaviour(board, startX, startY, this);

        return result;
    }

    public static List<Coordinate> MovementBehaviour(Board board, int startX, int startY, FigureBase currentFigure)
    {
        var result = new List<Coordinate>();

        for (int x = startX + 1, y = startY + 1; Coordinate.IsValid(x, y); x++, y++)
            if (CreateMovement(board, currentFigure, x, y, result))
                break;

        for (int x = startX - 1, y = startY - 1; Coordinate.IsValid(x, y); x--, y--)
            if (CreateMovement(board, currentFigure, x, y, result))
                break;

        for (int x = startX + 1, y = startY - 1; Coordinate.IsValid(x, y); x++, y--)
            if (CreateMovement(board, currentFigure, x, y, result))
                break;

        for (int x = startX - 1, y = startY + 1; Coordinate.IsValid(x, y); x--, y++)
            if (CreateMovement(board, currentFigure, x, y, result))
                break;

        return result;
    }
}