using Ardalis.Result;
using CmScheme.Masters.Application.Features.Location.Divisions.UpdateDivision;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class UpdateDivisionCommandHandlerTests
{
    private static UpdateDivisionCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_Division_Updates_Fields_And_Returns_NoContent()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.Divisions.Add(new Division { StateId = 1, DivisionName = "Old", DivisionCode = "OL" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var divisionId = await ctx.Divisions.Select(d => d.DivisionId).FirstAsync();

        var result = await handler.Handle(new UpdateDivisionCommand
        {
            DivisionId = divisionId,
            StateId = 2,
            DivisionName = "New",
            DivisionCode = "NW"
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var updated = await ctx.Divisions.FirstAsync(d => d.DivisionId == divisionId);
        Assert.Equal(2, updated.StateId);
        Assert.Equal("New", updated.DivisionName);
        Assert.Equal("NW", updated.DivisionCode);
    }

    [Fact]
    public async Task Handle_Missing_Division_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new UpdateDivisionCommand
        {
            DivisionId = 999,
            StateId = 1,
            DivisionName = "X",
            DivisionCode = "X"
        }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}