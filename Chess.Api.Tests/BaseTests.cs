using Chess.Db;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.Api.Tests;

//2 user
//1 user create lobby
//2 join lobby
//1 user start game
public static class Context
{
    public static HttpClient UserTwoClient { get; set; } = null!;
    public static HttpClient UserOneClient { get; set; } = null!;
    public static IServiceProvider Services { get; set; } = null!;
    public static ChessDbContext DbContext { get; set; } = null!;
}

public class BaseTests
{
    private WebApplicationFactory<Program> _application = null!;

    [SetUp]
    public void Setup()
    {
        _application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(_ => { PrepareEnv(_); });
        Context.UserOneClient = _application.CreateDefaultClient();
        Context.UserTwoClient = _application.CreateClient();
        Context.Services = _application.Services;
        Context.DbContext = Context.Services.GetRequiredService<ChessDbContext>();
    }


    protected virtual IWebHostBuilder PrepareEnv(IWebHostBuilder _)
    {
        return _.UseEnvironment("test");
    }
}