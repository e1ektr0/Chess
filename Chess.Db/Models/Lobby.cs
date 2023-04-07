using System.ComponentModel.DataAnnotations.Schema;

namespace Chess.Db.Models;

public enum LobbyStatus
{
    New,
    InGame,
    Completed
}

public class Lobby
{
    public long Id { get; set; }
    public string CreatorUserId { get; set; } = null!;
    [ForeignKey(nameof(CreatorUserId))]
    public User? CreatorUser { get; set; }

    public LobbyStatus Status { get; set; }
    public string? OpponentUserId { get; set; } 
    [ForeignKey(nameof(OpponentUserId))]
    public User?  OpponentUser { get; set; }

}