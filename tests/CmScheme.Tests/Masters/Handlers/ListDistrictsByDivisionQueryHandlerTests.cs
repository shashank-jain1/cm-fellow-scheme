using CmScheme.Masters.Application.Features.Location.Districts.ListDistrictsByDivision;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;

namespace CmScheme.Tests.Masters.Handlers;

public class ListDistrictsByDivisionQueryHandlerTests
{
    private static ListDistrictsByDivisionQueryHandler CreateHandler(MastersQueryDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_With_DivisionId_Filters_Districts()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.Districts.AddRange(
                new District { DivisionId = 1, DistrictName = "Belagavi" },
                new District { DivisionId = 1, DistrictName = "Dharwad" },
                new District { DivisionId = 2, DistrictName = "Indore" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListDistrictsByDivisionQuery { DivisionId = 1 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
        Assert.All(result.Value, d => Assert.Equal(1, d.DivisionId));
    }

    [Fact]
    public async Task Handle_Without_DivisionId_Returns_All_Districts_Ordered_By_Name()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.Districts.AddRange(
                new District { DivisionId = 1, DistrictName = "Zebra" },
                new District { DivisionId = 2, DistrictName = "Alpha" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListDistrictsByDivisionQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
        Assert.Equal("Alpha", result.Value[0].DistrictName);
        Assert.Equal("Zebra", result.Value[1].DistrictName);
    }

    [Fact]
    public async Task Handle_No_Matching_Division_Returns_Empty_List()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.Districts.Add(new District { DivisionId = 1, DistrictName = "Belagavi" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListDistrictsByDivisionQuery { DivisionId = 99 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
}