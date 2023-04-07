using Chess.Db;
using Chess.Db.Models;
using Chess.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chess.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LobbyController : ControllerBase
{
    private readonly LobbyService _lobbyService;
    private readonly ChessDbContext _dbContext;

    public LobbyController(LobbyService lobbyService, ChessDbContext dbContext)
    {
        _lobbyService = lobbyService;
        _dbContext = dbContext;
    }

    [HttpGet]
    [Authorize]
    public async Task<List<Lobby>> Get()
    {
        return await _dbContext.Lobbies.ToListAsync();
    }

    [HttpPost]
    [Authorize]
    public async Task<Lobby> CreateLobby()
    {
        return await _lobbyService.CreateLobby(User.UserId());
    }

    [HttpPost]
    [Authorize]
    [Route("{lobbyId}/Join")]
    public async Task<IActionResult> Join([FromRoute] long lobbyId)
    {
        await _lobbyService.JoinLobby(lobbyId, User.UserId());
        return Ok();
    }
}