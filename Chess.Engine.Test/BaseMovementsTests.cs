namespace Chess.Engine.Test;

public class BaseMovementsTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CreateNewBoard()
    {
        var board = new Board();

        Assert.That(board.CurrentPlayerColor, Is.EqualTo(PlayerColor.White));
        Assert.That(board.Figures, Is.Not.Empty);
        Assert.That(board.Figures.Count, Is.EqualTo(32));
    }


    [Test]
    public void PowMoveForward()
    {
        var board = new Board();

        var result = board.Move(0, 1, 0, 3);

        Assert.That(result);
        Assert.That(board.Figures.Any(n => n.Y == 3));
        Assert.That(board.CurrentPlayerColor, Is.EqualTo(PlayerColor.Black));
    }


    [Test]
    public void WrongPlayer()
    {
        var board = new Board();

        var result = board.Move(0, 6, 0, 7);

        Assert.That(result, Is.False);
    }


    [Test]
    public void WrongMovement()
    {
        var board = new Board();

        var result = board.Move(0, 1, 0, 5);

        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckEat()
    {
        var board = new Board();

        Assert.That(board.Move(0, 1, 0, 3)); //white pawn
        Assert.That(board.Move(1, 6, 1, 4)); //black pawn
        Assert.That(board.Move(0, 3, 1, 4)); //white pawn eat black pawn


        Assert.That(board.Figures.Count, Is.EqualTo(31));
    }


    [Test]
    public void MoveInsideOurFigureNotAllowed()
    {
        var board = new Board();
        var whiteQueen = board.Figures.First(n => n.Type == Figure.Queen && n.Color == PlayerColor.White);

        //move white queen forward to our pawn
        var result = board.Move(whiteQueen.X, whiteQueen.Y, whiteQueen.X, whiteQueen.Y + 1);

        Assert.That(result, Is.False);
    }
}