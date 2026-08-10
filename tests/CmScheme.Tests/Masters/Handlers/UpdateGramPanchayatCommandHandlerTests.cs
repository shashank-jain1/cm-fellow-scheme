using Ardalis.Result;
using CmScheme.Masters.Application.Features.Location.GramPanchayats.UpdateGramPanchayat;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class UpdateGramPanchayatCommandHandlerTests
{
    private static UpdateGramPanchayatCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_GramPanchayat_Updates_Fields_And_Returns_NoContent()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.GramPanchayats.Add(new GramPanchayat { BlockId = 1, GramPanchayatName = "Old", GPCode = "OL" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var gpId = await ctx.GramPanchayats.Select(g => g.GramPanchayatId).FirstAsync();

        var result = await handler.Handle(new UpdateGramPanchayatCommand
        {
            GramPanchayatId = gpId,
            BlockId = 2,
            GramPanchayatName = "New",
            GPCode = "NW"
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var updated = await ctx.GramPanchayats.FirstAsync(g => g.GramPanchayatId == gpId);
        Assert.Equal(2, updated.BlockId);
        Assert.Equal("New", updated.GramPanchayatName);
        Assert.Equal("NW", updated.GPCode);
    }

    [Fact]
    public async Task Handle_Missing_GramPanchayat_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new UpdateGramPanchayatCommand
        {
            GramPanchayatId = 999,
            BlockId = 1,
            GramPanchayatName = "X",
            GPCode = "X"
        }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}