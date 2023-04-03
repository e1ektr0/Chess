namespace Chess.Engine.Test;

public class CheckTests
{
    [SetUp]
    public void Setup()
    {
    }

    //todo: pawn moved to end of board and change to new figure
    //todo: en passant
    //todo: сastling
    
    [Test]
    public void CheckMat()
    {
        var board = new Board();

        Assert.That(board.Move(6, 1, 6, 3)); //white pawn move forward
        Assert.That(board.Move(4, 6, 4, 4)); //black pawn move forward
        Assert.That(board.Move(5, 1, 5, 2)); //white pawn move forward 
        Assert.That(board.Move(3, 7, 7, 3)); //black queen make mat
        Console.WriteLine(board);
        Assert.That(board.Status, Is.EqualTo(BoardStatus.Mat));
    }
    
    [Test]
    public void PlayerMustSaveKingIfItUnderTheCheck()
    {
        var board = new Board();

        Assert.That(board.Move(4, 1, 4, 3)); //white pawn move forward
        Assert.That(board.Move(5, 6, 5, 4)); //black pawn move forward
        Assert.That(board.Move(3, 0, 7, 4)); //white queen make check
        
        Assert.That(board.Move(0, 6, 0, 4), Is.False); //black player ignore check and move some random figure
    }
    
    [Test]
    public void PlayerSaveKing()
    {
        var board = new Board();

        Assert.That(board.Move(4, 1, 4, 3)); //white pawn move forward
        Assert.That(board.Move(5, 6, 5, 4)); //black pawn move forward
        Assert.That(board.Move(3, 0, 7, 4)); //white queen make check
        Assert.That(board.Move(6, 6, 6, 5), Is.True); //black player ignore check and move some random figure
    }


    [Test]
    public void PlayerShouldNotTriggerCheckForHimself()
    {
        var board = new Board();

        Assert.That(board.Move(4, 1, 4, 3)); //white pawn move forward
        Assert.That(board.Move(0, 6, 0, 4)); //black pawn move forward
        Assert.That(board.Move(3, 0, 7, 4)); //white queen make check

        Console.WriteLine(board);
        Console.WriteLine(board.Status);
        Assert.That(board.Move(5, 6, 5, 4),  Is.False); //black pawn move forward and trigger check
    }

    [Test]
    public void MakeCheck()
    {
        var board = new Board();

        Assert.That(board.Move(4, 1, 4, 3)); //white pawn move forward
        Assert.That(board.Move(5, 6, 5, 4)); //black pawn move forward
        Assert.That(board.Move(3, 0, 7, 4)); //white queen make check

        Assert.That(board.Status, Is.EqualTo(BoardStatus.Check));
    }
}