using Chess.Api.Models;
using Chess.Db.Models;
using Chess.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    [Authorize]
    public async Task<User> GetMe()
    {
        var userId = User.UserId();
        return await _authService.GetUser(userId);
    }
    
    [HttpPost]
    public async Task<AuthResult> RegisterUser(RegisterModel registerModel)
    {
        var token = await _authService.CreateAsync(registerModel.Email, registerModel.Password);
        return new AuthResult
        {
            Token = token
        };
    }
}