using Ardalis.Result;
using CmScheme.Masters.Application.Features.Location.States.DeleteState;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class DeleteStateCommandHandlerTests
{
    private static DeleteStateCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_State_Deactivates_And_Returns_NoContent()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.States.Add(new State { StateName = "Karnataka", StateCode = "KA" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var stateId = await ctx.States.Select(s => s.StateId).FirstAsync();

        var result = await handler.Handle(new DeleteStateCommand { StateId = stateId }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        Assert.Equal(1, await ctx.States.CountAsync());
        var state = await ctx.States.FirstAsync();
        Assert.False(state.IsActive);
    }

    [Fact]
    public async Task Handle_Missing_State_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new DeleteStateCommand { StateId = 999 }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}