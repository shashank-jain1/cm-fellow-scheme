using Ardalis.Result;
using CmScheme.Masters.Application.Features.Location.Districts.DeleteDistrict;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class DeleteDistrictCommandHandlerTests
{
    private static DeleteDistrictCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_District_Deactivates_And_Returns_NoContent()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.Districts.Add(new District { DivisionId = 1, DistrictName = "Belagavi" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var districtId = await ctx.Districts.Select(d => d.DistrictId).FirstAsync();

        var result = await handler.Handle(new DeleteDistrictCommand { DistrictId = districtId }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var district = await ctx.Districts.FirstAsync();
        Assert.False(district.IsActive);
    }

    [Fact]
    public async Task Handle_Missing_District_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new DeleteDistrictCommand { DistrictId = 999 }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}