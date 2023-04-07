namespace Chess.Share;

public class JwtConfig
{
    public string Issuer { get; set; } = "Chess";
    public string Audience { get; set; } = "Chess";
    public string Secret { get; set; } = "Chess_secret_xxx";
}