using Ardalis.Result;
using CmScheme.Masters.Application.Features.Location.Divisions.DeleteDivision;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class DeleteDivisionCommandHandlerTests
{
    private static DeleteDivisionCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_Division_Deactivates_And_Returns_NoContent()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.Divisions.Add(new Division { StateId = 1, DivisionName = "Belagavi" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var divisionId = await ctx.Divisions.Select(d => d.DivisionId).FirstAsync();

        var result = await handler.Handle(new DeleteDivisionCommand { DivisionId = divisionId }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var division = await ctx.Divisions.FirstAsync();
        Assert.False(division.IsActive);
    }

    [Fact]
    public async Task Handle_Missing_Division_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new DeleteDivisionCommand { DivisionId = 999 }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}