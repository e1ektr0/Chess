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

    public DateTime? StartTime { get; set; }
    public string? Board { get; set; }
    public string? ZeroUserId { get; set; }
    public string? OneUserId { get; set; }
    public int CurrentPlayer { get; set; }
    public string? CurrentPlayerId { get; set; }
}