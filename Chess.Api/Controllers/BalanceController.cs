using System.Data;
using Chess.Api.Models;
using Chess.Db;
using Chess.Db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chess.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BalanceController : ControllerBase
{
    private readonly ChessDbContext _dbContext;
    private readonly ILogger<BalanceController> _logger;

    public BalanceController(ChessDbContext dbContext, ILogger<BalanceController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [Authorize]
    [HttpPost]
    public async Task<Ok> Deposit(DepositRequest registerModel)
    {
        var userId = User.UserId();
        var balance = await _dbContext.UserBalances.FirstOrDefaultAsync(n => n.UserId == userId);
        if (balance == null)
        {
            await _dbContext.UserBalances.AddAsync(new UserBalance
            {
                Balance = registerModel.Amount,
                UserId = userId
            });
        }
        else
        {
            balance.Balance += registerModel.Amount;
        }

        await _dbContext.SaveChangesAsync();
        return new Ok();
    }


    [Authorize]
    [HttpPost]
    [Route("Buy")]
    public async Task<Ok> Buy()
    {
        var cost = 100;
        var userId = User.UserId();

        var beginTransactionAsync = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        try
        {
            var balance = await _dbContext.UserBalances.FirstOrDefaultAsync(n => n.UserId == userId);
            if (balance == null)
                throw new NotImplementedException();

            if (balance.Balance < cost)
                throw new NotImplementedException();

            balance.Balance -= cost;
            await _dbContext.SaveChangesAsync();
        }
        finally
        {
            await beginTransactionAsync.CommitAsync();
        }
     
        return new Ok();
    }
}