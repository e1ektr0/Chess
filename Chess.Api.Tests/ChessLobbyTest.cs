using Chess.Api.Controllers;
using Chess.Db.Models;
using RAIT.Core;

namespace Chess.Api.Tests;

public class ChessLobbyTest : BaseTests
{

    [Test]
    public async Task CreateLobby()
    {
        await new AuthTests().Register();

        var lobby = await Context.UserOneClient.Rait<LobbyController>().Call(n => n.CreateLobby());

        Assert.That(lobby, Is.Not.Null);
        Assert.That(Context.DbContext.Lobbies.ToList(), Is.Not.Empty);
    }
    
    [Test]
    public async Task JoinLobby()
    {
        await CreateLobby();
        await new AuthTests().RegisterUserTwo();

        var lobbies = await Context.UserTwoClient.Rait<LobbyController>().Call(n => n.Get());
        var lobby = lobbies!.First();
        await Context.UserTwoClient.Rait<LobbyController>().Call(n => n.Join(lobby.Id));

        var dbLobby = Context.DbContext.Lobbies.First();
        await Context.DbContext.Entry(dbLobby).ReloadAsync();
        
        Assert.That(dbLobby.OpponentUserId, Is.Not.Null);
    }
    
    [Test]
    public async Task StartGame()
    {
        await JoinLobby();
        
        var dbLobby = Context.DbContext.Lobbies.First();
        await Context.UserOneClient.Rait<LobbyController>().Call(n => n.Start(dbLobby.Id));
        var lobby = await Context.UserTwoClient.Rait<LobbyController>().Call(n => n.Get(dbLobby.Id));

        Assert.That(lobby, Is.Not.Null);
        Assert.That(lobby!.Status, Is.EqualTo(LobbyStatus.InGame));
    }
    
}