using Ardalis.Result;
using CmScheme.Masters.Application.Features.Location.Blocks.DeleteBlock;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class DeleteBlockCommandHandlerTests
{
    private static DeleteBlockCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_Block_Deactivates_And_Returns_NoContent()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.Blocks.Add(new Block { DistrictId = 1, BlockName = "Chikodi" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var blockId = await ctx.Blocks.Select(b => b.BlockId).FirstAsync();

        var result = await handler.Handle(new DeleteBlockCommand { BlockId = blockId }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var block = await ctx.Blocks.FirstAsync();
        Assert.False(block.IsActive);
    }

    [Fact]
    public async Task Handle_Missing_Block_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new DeleteBlockCommand { BlockId = 999 }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}