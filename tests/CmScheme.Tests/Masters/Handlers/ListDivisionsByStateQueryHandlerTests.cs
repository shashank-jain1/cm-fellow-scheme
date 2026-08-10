using CmScheme.Masters.Application.Features.Location.Divisions.ListDivisionsByState;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;

namespace CmScheme.Tests.Masters.Handlers;

public class ListDivisionsByStateQueryHandlerTests
{
    private static ListDivisionsByStateQueryHandler CreateHandler(MastersQueryDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_With_StateId_Filters_Divisions()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.Divisions.AddRange(
                new Division { StateId = 1, DivisionName = "Belagavi" },
                new Division { StateId = 1, DivisionName = "Kalaburagi" },
                new Division { StateId = 2, DivisionName = "Indore" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListDivisionsByStateQuery { StateId = 1 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
        Assert.All(result.Value, d => Assert.Equal(1, d.StateId));
    }

    [Fact]
    public async Task Handle_Without_StateId_Returns_All_Divisions_Ordered_By_Name()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.Divisions.AddRange(
                new Division { StateId = 1, DivisionName = "Zebra" },
                new Division { StateId = 2, DivisionName = "Alpha" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListDivisionsByStateQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
        Assert.Equal("Alpha", result.Value[0].DivisionName);
        Assert.Equal("Zebra", result.Value[1].DivisionName);
    }

    [Fact]
    public async Task Handle_No_Matching_State_Returns_Empty_List()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.Divisions.Add(new Division { StateId = 1, DivisionName = "Belagavi" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListDivisionsByStateQuery { StateId = 99 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
}