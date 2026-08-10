using Ardalis.Result;
using CmScheme.Masters.Application.Features.Location.Blocks.UpdateBlock;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class UpdateBlockCommandHandlerTests
{
    private static UpdateBlockCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_Block_Updates_Fields_And_Returns_NoContent()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.Blocks.Add(new Block { DistrictId = 1, BlockName = "Old", BlockCode = "OL" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var blockId = await ctx.Blocks.Select(b => b.BlockId).FirstAsync();

        var result = await handler.Handle(new UpdateBlockCommand
        {
            BlockId = blockId,
            DistrictId = 2,
            BlockName = "New",
            BlockCode = "NW"
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var updated = await ctx.Blocks.FirstAsync(b => b.BlockId == blockId);
        Assert.Equal(2, updated.DistrictId);
        Assert.Equal("New", updated.BlockName);
        Assert.Equal("NW", updated.BlockCode);
    }

    [Fact]
    public async Task Handle_Missing_Block_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new UpdateBlockCommand
        {
            BlockId = 999,
            DistrictId = 1,
            BlockName = "X",
            BlockCode = "X"
        }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}