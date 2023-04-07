using System.Net.Http.Headers;
using Chess.Api.Controllers;
using Chess.Api.Models;
using Chess.Db;
using Microsoft.Extensions.DependencyInjection;
using RAIT.Core;

namespace Chess.Api.Tests;

public class AuthTests : BaseTests
{
    [Test]
    public async Task Register()
    {
        
        var authResult = await Context.DefaultClient.Rait<AuthController>().Call(n => n.RegisterUser(new RegisterModel
        {
            Email = "e1ektr0.xyz@gmail.com",
            Password = "Password"
        }));
        
        
        var chessDbContext = Context.Services.GetRequiredService<ChessDbContext>();
        Assert.That(chessDbContext.Users.Count(), Is.Not.Zero);
        
        Context.DefaultClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", authResult!.Token);
    }
    
    [Test]
    public async Task GetMe()
    {
        await Register();
        var call = await Context.DefaultClient.Rait<AuthController>().Call(n => n.GetMe());

        Assert.That(call, Is.Not.Null);
    }
}