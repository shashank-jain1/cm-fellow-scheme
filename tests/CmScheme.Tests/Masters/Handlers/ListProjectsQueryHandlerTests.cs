using CmScheme.Masters.Application.Features.Projects.ListProjects;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;

namespace CmScheme.Tests.Masters.Handlers;

public class ListProjectsQueryHandlerTests
{
    private static ListProjectsQueryHandler CreateHandler(MastersQueryDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Returns_Only_Active_Projects_Ordered_By_CreatedOn_Descending()
    {
        var ctx = TestDbContext.CreateMastersQuery(c =>
        {
            c.Projects.AddRange(
                new Project
                {
                    ProjectName = "Oldest",
                    ProjectCode = "P1",
                    DepartmentName = "PWD",
                    ProjectIncharge = "A",
                    BudgetApprovedBy = 1,
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 1, 1)
                },
                new Project
                {
                    ProjectName = "Newest",
                    ProjectCode = "P2",
                    DepartmentName = "PWD",
                    ProjectIncharge = "B",
                    BudgetApprovedBy = 1,
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 3, 1)
                },
                new Project
                {
                    ProjectName = "Inactive",
                    ProjectCode = "P3",
                    DepartmentName = "PWD",
                    ProjectIncharge = "C",
                    BudgetApprovedBy = 1,
                    IsActive = false,
                    CreatedOn = new DateTime(2026, 2, 1)
                });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListProjectsQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
        Assert.Equal("Newest", result.Value[0].ProjectName);
        Assert.Equal("Oldest", result.Value[1].ProjectName);
    }

    [Fact]
    public async Task Handle_Empty_Store_Returns_Empty_List()
    {
        var ctx = TestDbContext.CreateMastersQuery();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new ListProjectsQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
}