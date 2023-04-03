namespace Chess.Engine.Figures;

public class Knight : FigureBase
{
    public override List<Coordinate> GetPossibleMovements(Board board)
    {
        var result = new List<Coordinate>();
        AddPossibleMovement(board, this, result, X + 1, Y + 2);
        AddPossibleMovement(board, this, result, X + 2, Y + 1);
        AddPossibleMovement(board, this, result, X + 2, Y - 1);
        AddPossibleMovement(board, this, result, X + 1, Y - 2);
        AddPossibleMovement(board, this, result, X - 1, Y - 2);
        AddPossibleMovement(board, this, result, X - 2, Y - 1);
        AddPossibleMovement(board, this, result, X - 2, Y + 1);
        AddPossibleMovement(board, this, result, X - 1, Y + 2);
        return result;
    }
}