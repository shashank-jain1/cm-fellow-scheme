using CmScheme.Masters.Application.Features.Location.Blocks.ListBlocksByDistrict;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;

namespace CmScheme.Tests.Masters.Handlers;

public class ListBlocksByDistrictQueryHandlerTests
{
    private static ListBlocksByDistrictQueryHandler CreateHandler(MastersQueryDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_With_DistrictId_Filters_Blocks()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.Blocks.AddRange(
                new Block { DistrictId = 1, BlockName = "Chikodi" },
                new Block { DistrictId = 1, BlockName = "Khanapur" },
                new Block { DistrictId = 2, BlockName = "Indore" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListBlocksByDistrictQuery { DistrictId = 1 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
        Assert.All(result.Value, b => Assert.Equal(1, b.DistrictId));
    }

    [Fact]
    public async Task Handle_Without_DistrictId_Returns_All_Blocks_Ordered_By_Name()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.Blocks.AddRange(
                new Block { DistrictId = 1, BlockName = "Zebra" },
                new Block { DistrictId = 2, BlockName = "Alpha" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListBlocksByDistrictQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
        Assert.Equal("Alpha", result.Value[0].BlockName);
        Assert.Equal("Zebra", result.Value[1].BlockName);
    }

    [Fact]
    public async Task Handle_No_Matching_District_Returns_Empty_List()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.Blocks.Add(new Block { DistrictId = 1, BlockName = "Chikodi" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListBlocksByDistrictQuery { DistrictId = 99 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
}