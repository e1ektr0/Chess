using System.Security.Claims;

namespace Chess.Api;

public static class UserExtension
{
    public static string UserId(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.Claims.FirstOrDefault(n => n.Type == ClaimTypes.Sid);
        if (claim == null)
            throw new Exception("Unauthorized");
        return claim.Value;
    }
}