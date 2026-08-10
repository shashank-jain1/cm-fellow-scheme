using CmScheme.Masters.Application.Features.Works.ListWorksByProject;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;

namespace CmScheme.Tests.Masters.Handlers;

public class ListWorksByProjectQueryHandlerTests
{
    private static ListWorksByProjectQueryHandler CreateHandler(MastersQueryDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_With_ProjectId_Filters_Works_Ordered_By_CreatedOn_Descending()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.Works.AddRange(
                new Work
                {
                    ProjectId = 1,
                    WorkName = "Old work",
                    Priority = "Low",
                    AssignedTo = "Fellow1",
                    CreatedOn = new DateTime(2026, 1, 1)
                },
                new Work
                {
                    ProjectId = 1,
                    WorkName = "New work",
                    Priority = "High",
                    AssignedTo = "Fellow2",
                    CreatedOn = new DateTime(2026, 2, 1)
                },
                new Work
                {
                    ProjectId = 2,
                    WorkName = "Other project",
                    Priority = "Medium",
                    AssignedTo = "Fellow3",
                    CreatedOn = new DateTime(2026, 2, 1)
                });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListWorksByProjectQuery { ProjectId = 1 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
        Assert.All(result.Value, w => Assert.Equal(1, w.ProjectId));
        Assert.Equal("New work", result.Value[0].WorkName);
        Assert.Equal("Old work", result.Value[1].WorkName);
    }

    [Fact]
    public async Task Handle_Without_ProjectId_Returns_All_Works()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.Works.AddRange(
                new Work { ProjectId = 1, WorkName = "A", Priority = "High", AssignedTo = "F1" },
                new Work { ProjectId = 2, WorkName = "B", Priority = "High", AssignedTo = "F2" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListWorksByProjectQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
    }

    [Fact]
    public async Task Handle_No_Matching_Project_Returns_Empty_List()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.Works.Add(new Work { ProjectId = 1, WorkName = "A", Priority = "High", AssignedTo = "F1" });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListWorksByProjectQuery { ProjectId = 99 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
}