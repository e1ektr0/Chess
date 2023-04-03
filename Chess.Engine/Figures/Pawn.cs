namespace Chess.Engine.Figures;

public class Pawn : FigureBase
{
    public bool FirstMoved { get; set; }

    public override void Move(int x1, int y1)
    {
        base.Move(x1, y1);
        FirstMoved = true;
    }

    public override List<Coordinate> GetPossibleMovements(Board board)
    {
        var result = new List<Coordinate>();

        if (board.CurrentPlayerColor == PlayerColor.White)
        {
            var coordinate = new Coordinate(X, Y + 1);
            if (PawnCheckPossibleMovementForward(board, coordinate))
                result.Add(coordinate);

            if (!FirstMoved)
            {
                coordinate = new Coordinate(X, Y + 2);
                if (PawnCheckPossibleMovementForward(board, coordinate))
                    result.Add(coordinate);
            }

            coordinate = new Coordinate(X + 1, Y + 1);
            if (PawnCheckPossibleEat(board, this, coordinate))
                result.Add(coordinate);
            coordinate = new Coordinate(X - 1, Y + 1);
            if (PawnCheckPossibleEat(board, this, coordinate))
                result.Add(coordinate);
        }

        if (board.CurrentPlayerColor == PlayerColor.Black)
        {
            var coordinate = new Coordinate(X, Y - 1);
            if (PawnCheckPossibleMovementForward(board, coordinate))
                result.Add(coordinate);

            if (!FirstMoved)
            {
                coordinate = new Coordinate(X, Y - 2);
                if (PawnCheckPossibleMovementForward(board, coordinate))
                    result.Add(coordinate);
            }

            coordinate = new Coordinate(X + 1, Y - 1);
            if (PawnCheckPossibleEat(board, this, coordinate))
                result.Add(coordinate);
            coordinate = new Coordinate(X - 1, Y - 1);
            if (PawnCheckPossibleEat(board, this, coordinate))
                result.Add(coordinate);
        }

        return result;
    }

    internal bool PawnCheckPossibleEat(Board board, FigureBase currentFigure, Coordinate coordinate)
    {
        if (!coordinate.IsValid())
            return false;

        var figure = board.Get(coordinate);
        if (figure == null)
            return false;
        if (figure.Color == currentFigure.Color)
            return false;

        return true;
    }

    internal bool PawnCheckPossibleMovementForward(Board board, Coordinate coordinate)
    {
        if (!coordinate.IsValid())
            return false;

        var figure = board.Get(coordinate);
        if (figure != null)
            return false;

        return true;
    }
}