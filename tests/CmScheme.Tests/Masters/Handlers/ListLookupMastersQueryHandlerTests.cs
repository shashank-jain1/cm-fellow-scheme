using CmScheme.Masters.Application.Features.LookupMaster.ListLookupMasters;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;

namespace CmScheme.Tests.Masters.Handlers;

public class ListLookupMastersQueryHandlerTests
{
    private static ListLookupMastersQueryHandler CreateHandler(MastersQueryDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_With_MasterType_Filters_Only_Active_Items_Ordered_By_SortOrder_Then_Label()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.LookupMasters.AddRange(
                new LookupMaster { MasterType = "Priority", Label = "High", Value = "H", SortOrder = 3 },
                new LookupMaster { MasterType = "Priority", Label = "Medium", Value = "M", SortOrder = 2 },
                new LookupMaster { MasterType = "Priority", Label = "Low", Value = "L", SortOrder = 2 },
                new LookupMaster { MasterType = "Status", Label = "Active", Value = "A", SortOrder = 1 },
                new LookupMaster { MasterType = "Priority", Label = "Inactive", Value = "I", SortOrder = 1, IsActive = false });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListLookupMastersQuery { MasterType = "Priority" }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value.Count);
        Assert.Equal("Low", result.Value[0].Label);
        Assert.Equal("Medium", result.Value[1].Label);
        Assert.Equal("High", result.Value[2].Label);
        Assert.All(result.Value, lm => Assert.Equal("Priority", lm.MasterType));
    }

    [Fact]
    public async Task Handle_Without_MasterType_Returns_All_Items()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.LookupMasters.AddRange(
                new LookupMaster { MasterType = "Priority", Label = "High", Value = "H", SortOrder = 1 },
                new LookupMaster { MasterType = "Status", Label = "Active", Value = "A", SortOrder = 1 });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListLookupMastersQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
    }

    [Fact]
    public async Task Handle_Excludes_Inactive_Items()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.LookupMasters.AddRange(
                new LookupMaster { MasterType = "Priority", Label = "High", Value = "H", SortOrder = 1 },
                new LookupMaster { MasterType = "Priority", Label = "Old", Value = "O", SortOrder = 1, IsActive = false });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListLookupMastersQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Single(result.Value);
        Assert.Equal("High", result.Value[0].Label);
    }
}