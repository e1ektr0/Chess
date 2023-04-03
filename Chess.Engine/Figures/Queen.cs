namespace Chess.Engine.Figures;

public class Queen : FigureBase
{
    public override List<Coordinate> GetPossibleMovements(Board board)
    {
        var result = new List<Coordinate>();
        result.AddRange(Rook.MovementBehaviour(board, X, Y, this));
        result.AddRange(Bishop.MovementBehaviour(board, X, Y, this));
        return result;
    }
}