using Chess.Api.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using RAIT.Core;

namespace Chess.Api.Tests;

public class Tests
{
    private WebApplicationFactory<Program> _application;
    private HttpClient _defaultClient;

    [SetUp]
    public void Setup()
    {
        _application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(_ => { PrepareEnv(_); });
        _defaultClient = _application.CreateDefaultClient();
    }

    protected virtual IWebHostBuilder PrepareEnv(IWebHostBuilder _)
    {
        return _.UseEnvironment("test");
    }

    [Test]
    public async Task Test1()
    {
        var model = await _defaultClient.Rait<WeatherForecastController>().Call(n => n.Get());

        Assert.That(model, Is.Not.Null);
        Assert.That(model!.Summary, Is.Not.Empty);
    }
}