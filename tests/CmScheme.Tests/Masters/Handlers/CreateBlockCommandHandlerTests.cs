using CmScheme.Masters.Application.Features.Location.Blocks.CreateBlock;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class CreateBlockCommandHandlerTests
{
    private static CreateBlockCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Valid_Command_Creates_Block_And_Returns_NewId()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new CreateBlockCommand
        {
            DistrictId = 1,
            BlockName = "Chikodi",
            BlockCode = "CHI"
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.True(result.Value > 0);
        Assert.Equal(1, await ctx.Blocks.CountAsync());
        var block = await ctx.Blocks.FirstAsync();
        Assert.Equal(1, block.DistrictId);
        Assert.Equal("Chikodi", block.BlockName);
        Assert.Equal("CHI", block.BlockCode);
        Assert.True(block.IsActive);
    }
}