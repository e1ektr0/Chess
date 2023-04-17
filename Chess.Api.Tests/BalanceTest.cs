using Chess.Api.Controllers;
using Chess.Api.Models;
using Chess.Db.Models;
using RAIT.Core;

namespace Chess.Api.Tests;

public class BalanceTest : BaseTests
{
    [Test]
    public async Task SuccessBuy()
    {
        await new AuthTests().Register();

        await Context.UserOneClient.Rait<BalanceController>().Call(n => n.Deposit(new DepositRequest
        {
            Amount = 100
        }));

        await Context.UserOneClient.Rait<BalanceController>().Call(n => n.Buy());
    }


    [Test]
    public async Task FailBuy()
    {
        await new AuthTests().Register();

        await Context.UserOneClient.Rait<BalanceController>().Call(n => n.Deposit(new DepositRequest
        {
            Amount = 10
        }));

        Assert.ThrowsAsync<NotImplementedException>(async () =>
            await Context.UserOneClient.Rait<BalanceController>().Call(n => n.Buy()));
    }


    [Test]
    public async Task Buy11()
    {
        await new AuthTests().Register();

        await Context.UserOneClient.Rait<BalanceController>().Call(n => n.Deposit(new DepositRequest
        {
            Amount = 1000
        }));

        var countSuccess = 0;
        var tasks = new List<Task>();
        for (int i = 0; i < 20; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    await Context.UserOneClient.Rait<BalanceController>().Call(n => n.Buy());
                    countSuccess++;
                }
                catch (Exception)
                {
                }
            }));
        }

        for (int i = 0; i < 10; i++)
        {
            try
            {
                await Context.UserOneClient.Rait<BalanceController>().Call(n => n.Buy());
                countSuccess++;
            }
            catch (Exception)
            {
            }
        }


        Task.WaitAll(tasks.ToArray());
        Assert.That(countSuccess, Is.EqualTo(10));
    }
}