using Ardalis.Result;
using CmScheme.Masters.Application.Features.Location.States.UpdateState;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class UpdateStateCommandHandlerTests
{
    private static UpdateStateCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_State_Updates_Fields_And_Returns_NoContent()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.States.Add(new State { StateName = "Old", StateCode = "OL", IsActive = true });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var stateId = await ctx.States.Select(s => s.StateId).FirstAsync();

        var result = await handler.Handle(new UpdateStateCommand
        {
            StateId = stateId,
            StateName = "New",
            StateCode = "NW",
            StateShortName = "N",
            DisplayOrder = 5,
            IsActive = false
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        var updated = await ctx.States.FirstAsync(s => s.StateId == stateId);
        Assert.Equal("New", updated.StateName);
        Assert.Equal("NW", updated.StateCode);
        Assert.Equal("N", updated.StateShortName);
        Assert.Equal(5, updated.DisplayOrder);
        Assert.False(updated.IsActive);
    }

    [Fact]
    public async Task Handle_Missing_State_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new UpdateStateCommand
        {
            StateId = 999,
            StateName = "X",
            StateCode = "X",
            IsActive = true
        }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}