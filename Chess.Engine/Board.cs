using Chess.Engine.Figures;

namespace Chess.Engine;

public class Board
{
    public Board()
    {
        Ini();
    }

    public Board(List<FigureBase> figureBases)
    {
        Figures = figureBases;
    }

    public PlayerColor CurrentPlayerColor { get; set; }
    public List<FigureBase> Figures { get; set; } = new();
    public BoardStatus Status { get; set; }

    private void Ini()
    {
        for (var i = 0; i < 8; i++)
        {
            Figures.Add(FigureBase.Create(Figure.Pawn, PlayerColor.White, i, 1));
            Figures.Add(FigureBase.Create(Figure.Pawn, PlayerColor.Black, i, 6));
        }

        Figures.Add(FigureBase.Create(Figure.Rook, PlayerColor.White, 0, 0));
        Figures.Add(FigureBase.Create(Figure.Rook, PlayerColor.White, 7, 0));
        Figures.Add(FigureBase.Create(Figure.Knight, PlayerColor.White, 1, 0));
        Figures.Add(FigureBase.Create(Figure.Knight, PlayerColor.White, 6, 0));
        Figures.Add(FigureBase.Create(Figure.Bishop, PlayerColor.White, 2, 0));
        Figures.Add(FigureBase.Create(Figure.Bishop, PlayerColor.White, 5, 0));
        Figures.Add(FigureBase.Create(Figure.Queen, PlayerColor.White, 3, 0));
        Figures.Add(FigureBase.Create(Figure.King, PlayerColor.White, 4, 0));

        Figures.Add(FigureBase.Create(Figure.Rook, PlayerColor.Black, 0, 7));
        Figures.Add(FigureBase.Create(Figure.Rook, PlayerColor.Black, 7, 7));
        Figures.Add(FigureBase.Create(Figure.Knight, PlayerColor.Black, 1, 7));
        Figures.Add(FigureBase.Create(Figure.Knight, PlayerColor.Black, 6, 7));
        Figures.Add(FigureBase.Create(Figure.Bishop, PlayerColor.Black, 2, 7));
        Figures.Add(FigureBase.Create(Figure.Bishop, PlayerColor.Black, 5, 7));
        Figures.Add(FigureBase.Create(Figure.Queen, PlayerColor.Black, 3, 7));
        Figures.Add(FigureBase.Create(Figure.King, PlayerColor.Black, 4, 7));
    }

    public bool Move(int x0, int y0, int x1, int y1)
    {
        var figure = Figures.FirstOrDefault(n => n.X == x0 && n.Y == y0);
        if (figure == null)
            return false;

        var playerColor = CurrentPlayerColor;
        if (figure.Color != playerColor)
            return false;

        var movements = figure.GetPossibleMovements(this);

        if (!movements.Any(n => n.X == x1 && n.Y == y1))
            return false;


        //todo: check/shah , mat
        if (Simulate(x1, y1, playerColor, figure))
            Apply(x1, y1, playerColor, figure);
        else
            return false;
        return true;
    }

    private bool Simulate(int x1, int y1, PlayerColor playerColor, FigureBase figure)
    {
        var simulatedFigures = Figures.ToList();
        var enemyFigure = Get(new Coordinate(x1, y1));
        if (enemyFigure != null && enemyFigure.Color != playerColor)
            simulatedFigures.Remove(enemyFigure);

        var copyFigure = figure.Copy();
        simulatedFigures.Remove(figure);
        simulatedFigures.Add(copyFigure);
        copyFigure.Move(x1, y1);

        var boardStatus = CheckCheck(playerColor.Invert(), simulatedFigures);
        if (boardStatus == BoardStatus.Check)
            return false;
        return true;
    }

    private void Apply(int x1, int y1, PlayerColor playerColor, FigureBase figure)
    {
        //success:
        var enemyFigure = Get(new Coordinate(x1, y1));
        if (enemyFigure != null && enemyFigure.Color != playerColor)
            Figures.Remove(enemyFigure);
        figure.Move(x1, y1);

        CurrentPlayerColor = playerColor.Invert();
        //i done check:
        Status = CheckCheck(playerColor, Figures);
        CheckMat(playerColor);
    }

    private void CheckMat(PlayerColor playerColor)
    {
        if (Status != BoardStatus.Check) 
            return;
        
        //check mat
        var figures = Figures.Where(n => n.Color == playerColor.Invert());
        foreach (var figureBase in figures)
        {
            var movements = figureBase.GetPossibleMovements(this);
            foreach (var coordinate in movements)
            {
                if (Simulate(coordinate.X, coordinate.Y, playerColor.Invert(), figureBase))
                    return;
            }
        }

        Status = BoardStatus.Mat;
    }

    private BoardStatus CheckCheck(PlayerColor playerColor, List<FigureBase> figureBases)
    {
        var board = new Board(figureBases);
        var ourFigures = figureBases.Where(n => n.Color == playerColor)
            .Where(n => n.Type == Figure.Queen); //todo:
        foreach (var figure in ourFigures)
        {
            var possibleMovements = figure.GetPossibleMovements(board);
            foreach (var possibleMovement in possibleMovements)
            {
                var targetFigure = Get(possibleMovement);
                if (targetFigure == null ||
                    targetFigure.Color == playerColor ||
                    targetFigure.Type != Figure.King)
                    continue;

                return BoardStatus.Check;
            }
        }

        return BoardStatus.OnTheFly;
    }

    public FigureBase? Get(Coordinate coordinate)
    {
        return Figures.FirstOrDefault(n => n.X == coordinate.X && n.Y == coordinate.Y);
    }

    public override string ToString()
    {
        var result = "";
        for (var y = 7; y >= 0; y--)
        {
            for (var x = 0; x < 8; x++)
            {
                var figure = Get(new Coordinate(x, y));
                if (figure == null)
                    result += "\t";
                else
                {
                    var symbol = figure.Type;
                    if (figure.Color == PlayerColor.Black)
                        symbol += '♚' - '♔';
                    result += (char)symbol + "\t";
                }
            }

            result += Environment.NewLine;
        }

        return result;
    }
}