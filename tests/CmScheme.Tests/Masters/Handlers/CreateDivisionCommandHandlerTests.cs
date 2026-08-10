using CmScheme.Masters.Application.Features.Location.Divisions.CreateDivision;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class CreateDivisionCommandHandlerTests
{
    private static CreateDivisionCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Valid_Command_Creates_Division_And_Returns_NewId()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new CreateDivisionCommand
        {
            StateId = 1,
            DivisionName = "Belagavi",
            DivisionCode = "BLR"
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.True(result.Value > 0);
        Assert.Equal(1, await ctx.Divisions.CountAsync());
        var division = await ctx.Divisions.FirstAsync();
        Assert.Equal(1, division.StateId);
        Assert.Equal("Belagavi", division.DivisionName);
        Assert.Equal("BLR", division.DivisionCode);
        Assert.True(division.IsActive);
    }
}