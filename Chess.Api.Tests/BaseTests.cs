using Chess.Api.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Chess.Api.Tests;

//2 user
//1 user create lobby
//2 join lobby
//1 user start game

public class BaseTests
{
    private WebApplicationFactory<Program> _application = null!;
    protected HttpClient DefaultClient = null!;
    protected IServiceProvider Services = null!;

    [SetUp]
    public void Setup()
    {
        _application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(_ => { PrepareEnv(_); });
        DefaultClient = _application.CreateDefaultClient();
        Services = _application.Services;
    }


    protected virtual IWebHostBuilder PrepareEnv(IWebHostBuilder _)
    {
        return _.UseEnvironment("test");
    }
}