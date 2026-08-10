using Ardalis.Result;
using CmScheme.Masters.Application.Features.Location.Districts.UpdateDistrict;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class UpdateDistrictCommandHandlerTests
{
    private static UpdateDistrictCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_District_Updates_Fields_And_Returns_NoContent()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.Districts.Add(new District { DivisionId = 1, DistrictName = "Old", DistrictCode = "OL" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var districtId = await ctx.Districts.Select(d => d.DistrictId).FirstAsync();

        var result = await handler.Handle(new UpdateDistrictCommand
        {
            DistrictId = districtId,
            DivisionId = 2,
            DistrictName = "New",
            DistrictCode = "NW"
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var updated = await ctx.Districts.FirstAsync(d => d.DistrictId == districtId);
        Assert.Equal(2, updated.DivisionId);
        Assert.Equal("New", updated.DistrictName);
        Assert.Equal("NW", updated.DistrictCode);
    }

    [Fact]
    public async Task Handle_Missing_District_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new UpdateDistrictCommand
        {
            DistrictId = 999,
            DivisionId = 1,
            DistrictName = "X",
            DistrictCode = "X"
        }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}