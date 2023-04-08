namespace Chess.Api.Models;


public class ChessGameMoveRequest
{
    public long LobbyId { get; set; }
    public int X0 { get; set; }
    public int Y0 { get; set; }
    public int X1 { get; set; }
    public int Y1 { get; set; }
}