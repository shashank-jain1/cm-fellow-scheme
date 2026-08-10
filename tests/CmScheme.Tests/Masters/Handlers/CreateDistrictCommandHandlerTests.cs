using CmScheme.Masters.Application.Features.Location.Districts.CreateDistrict;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class CreateDistrictCommandHandlerTests
{
    private static CreateDistrictCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Valid_Command_Creates_District_And_Returns_NewId()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new CreateDistrictCommand
        {
            DivisionId = 1,
            DistrictName = "Belagavi",
            DistrictCode = "BEL"
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.True(result.Value > 0);
        Assert.Equal(1, await ctx.Districts.CountAsync());
        var district = await ctx.Districts.FirstAsync();
        Assert.Equal(1, district.DivisionId);
        Assert.Equal("Belagavi", district.DistrictName);
        Assert.Equal("BEL", district.DistrictCode);
        Assert.True(district.IsActive);
    }
}