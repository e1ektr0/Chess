using Chess.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.Db;

public class ChessDbContextInitializer
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly GlobalConfig _globalConfig;

    public ChessDbContextInitializer(IServiceScopeFactory scopeFactory, GlobalConfig globalConfig)
    {
        _scopeFactory = scopeFactory;
        _globalConfig = globalConfig;
    }

    public async Task Initialize()
    {
        await using var asyncScope = _scopeFactory.CreateAsyncScope();
        var chessDbContext = asyncScope.ServiceProvider.GetRequiredService<ChessDbContext>();
        if(_globalConfig.ChessDbReset)
            await chessDbContext.Database.EnsureDeletedAsync();
        
        await chessDbContext.Database.MigrateAsync();
    }
}