using Chess.Db.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chess.Db;

public class ChessDbContext : IdentityDbContext<User>
{
    private readonly bool _initialized;

    public ChessDbContext()
    {
    }

    public ChessDbContext(DbContextOptions<ChessDbContext> options) : base(options)
    {
        _initialized = true;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!_initialized)
            optionsBuilder.UseNpgsql();
    }
}