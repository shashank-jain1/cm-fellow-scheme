using Ardalis.Result;
using CmScheme.Masters.Application.Features.Location.GramPanchayats.DeleteGramPanchayat;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class DeleteGramPanchayatCommandHandlerTests
{
    private static DeleteGramPanchayatCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_GramPanchayat_Deactivates_And_Returns_NoContent()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.GramPanchayats.Add(new GramPanchayat { BlockId = 1, GramPanchayatName = "Kagwad" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var gpId = await ctx.GramPanchayats.Select(g => g.GramPanchayatId).FirstAsync();

        var result = await handler.Handle(new DeleteGramPanchayatCommand { GramPanchayatId = gpId }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var gp = await ctx.GramPanchayats.FirstAsync();
        Assert.False(gp.IsActive);
    }

    [Fact]
    public async Task Handle_Missing_GramPanchayat_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new DeleteGramPanchayatCommand { GramPanchayatId = 999 }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}