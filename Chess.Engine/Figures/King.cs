namespace Chess.Engine.Figures;

public class King : FigureBase
{
    public override List<Coordinate> GetPossibleMovements(Board board)
    {
        var result = new List<Coordinate>();
        AddPossibleMovement(board, this, result, X, Y + 1);
        AddPossibleMovement(board, this, result, X, Y - 1);
        AddPossibleMovement(board, this, result, X + 1, Y);
        AddPossibleMovement(board, this, result, X - 1, Y);

        return result;
    }
}