using CmScheme.Masters.Application.Features.Location.GramPanchayats.CreateGramPanchayat;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class CreateGramPanchayatCommandHandlerTests
{
    private static CreateGramPanchayatCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Valid_Command_Creates_GramPanchayat_And_Returns_NewId()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new CreateGramPanchayatCommand
        {
            BlockId = 1,
            GramPanchayatName = "Kagwad",
            GPCode = "KGW"
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.True(result.Value > 0);
        Assert.Equal(1, await ctx.GramPanchayats.CountAsync());
        var gp = await ctx.GramPanchayats.FirstAsync();
        Assert.Equal(1, gp.BlockId);
        Assert.Equal("Kagwad", gp.GramPanchayatName);
        Assert.Equal("KGW", gp.GPCode);
        Assert.True(gp.IsActive);
    }
}