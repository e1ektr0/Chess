using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chess.Db.Models;
using Chess.Share;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Chess.Services;

public class JwtTokenService
{
    private readonly GlobalConfig _globalConfig;
    private readonly UserManager<User> _userManager;

    public JwtTokenService(GlobalConfig globalConfig, UserManager<User> userManager)
    {
        _globalConfig = globalConfig;
        _userManager = userManager;
    }

    public async Task<string> CreateTokenAsync(User user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_globalConfig.JwtConfig.Secret);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Sid, user.Id),
        };
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken
        (
            issuer: _globalConfig.JwtConfig.Issuer,
            audience: _globalConfig.JwtConfig.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(10)),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

}