using Chess.Api.Controllers;
using Chess.Api.Models;
using RAIT.Core;

namespace Chess.Api.Tests;

public class ChessGameTest : BaseTests
{
    private HttpClient _currentClient = null!;

    [Test]
    public async Task FirstMovePawn()
    {
        await new ChessLobbyTest().StartGame();

        var dbLobby = Context.DbContext.Lobbies.First();
        var lobby = await Context.UserTwoClient.Rait<LobbyController>().Call(n => n.Get(dbLobby.Id));

        var zeroUser = Context.DbContext.Users.First(n => n.Id == lobby!.ZeroUserId);
        _currentClient = Context.UserOneClient;
        if (zeroUser.UserName == "nyka@gmail.com")
            _currentClient = Context.UserTwoClient;

        await _currentClient.Rait<LobbyController>().Call(n => n.Move(new ChessGameMoveRequest
        {
            X0 = 0,
            Y0 = 1,
            X1 = 0,
            Y1 = 3,
            LobbyId = lobby!.Id
        }));

        var currentPlayerId = lobby!.CurrentPlayerId;
        lobby = await Context.UserTwoClient.Rait<LobbyController>().Call(n => n.Get(dbLobby.Id));
        Assert.That(lobby!.CurrentPlayerId, Is.Not.EqualTo(currentPlayerId));
    }

    [Test]
    public async Task SecondMovePawn()
    {
        await FirstMovePawn();
        _currentClient = _currentClient.Next();

        var dbLobby = Context.DbContext.Lobbies.First();

        await _currentClient.Rait<LobbyController>().Call(n => n.Move(new ChessGameMoveRequest
        {
            X0 = 1,
            Y0 = 6,
            X1 = 1,
            Y1 = 4,
            LobbyId = dbLobby.Id
        }));
    }
}