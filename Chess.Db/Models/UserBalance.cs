using System.ComponentModel.DataAnnotations.Schema;

namespace Chess.Db.Models;

public class UserBalance
{
    public long Id { get; set; }
    public decimal Balance { get; set; }
    public string UserId { get; set; } = null!;
    [ForeignKey(nameof(UserId))] public User? User { get; set; }
}