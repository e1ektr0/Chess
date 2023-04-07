using System.Text.Json;
using Chess.Db;
using Chess.Db.Models;
using Chess.Engine;
using Microsoft.EntityFrameworkCore;

namespace Chess.Services;

public class LobbyService
{
    private readonly ChessDbContext _dbContext;

    public LobbyService(ChessDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Lobby> CreateLobby(string creatorId)
    {
        var lobby = new Lobby
        {
            CreatorUserId = creatorId
        };
        await _dbContext.Lobbies.AddAsync(lobby);
        await _dbContext.SaveChangesAsync();
        return lobby;
    }

    public async Task JoinLobby(long lobbyId, string userId)
    {
        var lobby = await _dbContext.Lobbies.FirstOrDefaultAsync(n => n.Id == lobbyId);
        if (lobby == null)
            throw new Exception("Lobby not found");

        if (lobby.Status != LobbyStatus.New)
            throw new Exception("Lobby wrong status");

        if (lobby.OpponentUserId != null)
            throw new Exception("Lobby don't have slots");

        lobby.OpponentUserId = userId;

        await _dbContext.SaveChangesAsync();
    }

    public async Task StartLobby(long lobbyId, string userId)
    {
        var lobby = await _dbContext.Lobbies.FirstOrDefaultAsync(n => n.Id == lobbyId);
        if (lobby == null)
            throw new Exception("Lobby not found");

        if (lobby.Status != LobbyStatus.New)
            throw new Exception("Lobby wrong status");

        if (lobby.OpponentUserId == null)
            throw new Exception("No opponent. Start not allowed");

        if (lobby.CreatorUserId != userId)
            throw new Exception("Only creator can start lobby");

        lobby.Status = LobbyStatus.InGame;
        lobby.StartTime = DateTime.UtcNow;
        var board = new Board();
        lobby.Board = JsonSerializer.Serialize(board);

        var rnd = new Random();
        var next = rnd.Next(1);
        if (next == 1)
        {
            lobby.ZeroUserId = lobby.CreatorUserId;
            lobby.OneUserId = lobby.OpponentUserId;
        }
        else
        {
            lobby.ZeroUserId = lobby.OpponentUserId;
            lobby.OneUserId = lobby.CreatorUserId;
        }

        await _dbContext.SaveChangesAsync();
    }
}