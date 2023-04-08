using Chess.Db;
using Chess.Db.Models;
using Chess.Engine;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Chess.Services;

public class LobbyService
{
    private readonly ChessDbContext _dbContext;

    private static readonly JsonSerializerSettings Settings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

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

        lobby.Board = JsonConvert.SerializeObject(board, Settings);

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
        lobby.CurrentPlayerId = GetCurrentUser(lobby, board.CurrentPlayerColor);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Move(string userId, long lobbyId, int x0, int x1, int y0, int y1)
    {
        var lobby = await _dbContext.Lobbies.FirstOrDefaultAsync(n => n.Id == lobbyId);
        if (lobby == null)
            throw new Exception("Lobby not found");

        if (lobby.Status != LobbyStatus.InGame)
            throw new Exception("Lobby wrong status");

        var board = JsonConvert.DeserializeObject<Board>(lobby.Board, Settings);

        var currentUserId = GetCurrentUser(lobby, board.CurrentPlayerColor);
        if (currentUserId != userId)
            throw new Exception("Wrong player");

        var result = board.Move(x0, y0, x1, y1);
        if (!result)
            throw new Exception("Wrong move");

        lobby.Board = JsonConvert.SerializeObject(board, Settings);
        lobby.CurrentPlayer = (int)board.CurrentPlayerColor;
        lobby.CurrentPlayerId = GetCurrentUser(lobby, board.CurrentPlayerColor);
        await _dbContext.SaveChangesAsync();
    }

    private string GetCurrentUser(Lobby lobby, PlayerColor boardCurrentPlayerColor)
    {
        if (boardCurrentPlayerColor == PlayerColor.White)
            return lobby.ZeroUserId!;
        return lobby.OneUserId!;
    }
}