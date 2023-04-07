using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chess.Db.Models;
using Chess.Share;
using Microsoft.AspNetCore.Identity;

namespace Chess.Services;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtTokenService _jwtTokenService;

    public AuthService(UserManager<User> userManager, JwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<string> CreateAsync(string email, string password)
    {
        var user = new User
        {
            Email = email,
            UserName = email
        };
        var identityResult = await _userManager.CreateAsync(user, password);
        if (identityResult.Succeeded)
            return await _jwtTokenService.CreateTokenAsync(user);

        //handel error
        var identityError = identityResult.Errors.First();
        throw new Exception($"{identityError.Code} {identityError.Description}");
    }

    public async Task<User> GetUser(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }
}