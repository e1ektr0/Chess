namespace Chess.Share;

public class GlobalConfig
{
    public string ChessConnectionString { get; set; } = null!;
    public bool ChessDbReset { get; set; }
}