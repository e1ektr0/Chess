using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.Db;

public class ChessDbContextInitializer
{
    private readonly IServiceScopeFactory _scopeFactory;

    public ChessDbContextInitializer(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task Initialize()
    {
        await using var asyncScope = _scopeFactory.CreateAsyncScope();
        var chessDbContext = asyncScope.ServiceProvider.GetRequiredService<ChessDbContext>();
        await chessDbContext.Database.EnsureCreatedAsync();
        await chessDbContext.Database.MigrateAsync();
    }
}