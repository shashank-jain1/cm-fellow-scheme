using CmScheme.Masters.Application.Features.Location.States.CreateState;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class CreateStateCommandHandlerTests
{
    private static CreateStateCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Valid_Command_Creates_State_And_Returns_NewId()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new CreateStateCommand
        {
            StateName = "Karnataka",
            StateCode = "KA",
            StateShortName = "KAR"
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.True(result.Value > 0);
        Assert.Equal(1, await ctx.States.CountAsync());
        var state = await ctx.States.FirstAsync();
        Assert.Equal("Karnataka", state.StateName);
        Assert.Equal("KA", state.StateCode);
        Assert.Equal("KAR", state.StateShortName);
        Assert.True(state.IsActive);
    }

    [Fact]
    public async Task Handle_Command_Persists_DisplayOrder()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new CreateStateCommand
        {
            StateName = "Madhya Pradesh",
            StateCode = "MP",
            DisplayOrder = 3
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        var state = await ctx.States.FirstAsync();
        Assert.Equal(3, state.DisplayOrder);
    }
}