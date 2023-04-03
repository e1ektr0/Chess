using Chess.Db;
using Chess.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configs
builder.Services.Configure<GlobalConfig>(builder.Configuration.Bind);
builder.Services.AddSingleton(n => n.GetService<IOptions<GlobalConfig>>()!.Value);

//db
builder.Services.AddDbContext<ChessDbContext>((s, n) =>
    n.UseNpgsql(s.GetRequiredService<GlobalConfig>().ChessConnectionString));
builder.Services.AddSingleton<ChessDbContextInitializer>();


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.Services.GetRequiredService<ChessDbContextInitializer>().Initialize();

app.Run();