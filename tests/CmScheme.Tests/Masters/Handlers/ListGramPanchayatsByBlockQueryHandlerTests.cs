using CmScheme.Masters.Application.Features.Location.GramPanchayats.ListGramPanchayatsByBlock;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;

namespace CmScheme.Tests.Masters.Handlers;

public class ListGramPanchayatsByBlockQueryHandlerTests
{
    private static ListGramPanchayatsByBlockQueryHandler CreateHandler(MastersQueryDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_With_BlockId_Filters_GramPanchayats()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.GramPanchayats.AddRange(
                new GramPanchayat { BlockId = 1, GramPanchayatName = "Kagwad" },
                new GramPanchayat { BlockId = 1, GramPanchayatName = "Raibag" },
                new GramPanchayat { BlockId = 2, GramPanchayatName = "Indore" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListGramPanchayatsByBlockQuery { BlockId = 1 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
        Assert.All(result.Value, g => Assert.Equal(1, g.BlockId));
    }

    [Fact]
    public async Task Handle_Without_BlockId_Returns_All_GramPanchayats_Ordered_By_Name()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.GramPanchayats.AddRange(
                new GramPanchayat { BlockId = 1, GramPanchayatName = "Zebra" },
                new GramPanchayat { BlockId = 2, GramPanchayatName = "Alpha" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListGramPanchayatsByBlockQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
        Assert.Equal("Alpha", result.Value[0].GramPanchayatName);
        Assert.Equal("Zebra", result.Value[1].GramPanchayatName);
    }

    [Fact]
    public async Task Handle_No_Matching_Block_Returns_Empty_List()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.GramPanchayats.Add(new GramPanchayat { BlockId = 1, GramPanchayatName = "Kagwad" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListGramPanchayatsByBlockQuery { BlockId = 99 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
}