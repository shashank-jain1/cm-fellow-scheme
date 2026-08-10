using CmScheme.Masters.Application.Features.LookupMaster.CreateLookupMaster;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class CreateLookupMasterCommandHandlerTests
{
    private static CreateLookupMasterCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Valid_Command_Creates_LookupMaster_And_Returns_NewId()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new CreateLookupMasterCommand
        {
            MasterType = "Priority",
            Label = "High",
            Value = "H",
            SortOrder = 1
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.True(result.Value > 0);
        Assert.Equal(1, await ctx.LookupMasters.CountAsync());
        var lookup = await ctx.LookupMasters.FirstAsync();
        Assert.Equal("Priority", lookup.MasterType);
        Assert.Equal("High", lookup.Label);
        Assert.Equal("H", lookup.Value);
        Assert.Equal(1, lookup.SortOrder);
        Assert.True(lookup.IsActive);
    }
}