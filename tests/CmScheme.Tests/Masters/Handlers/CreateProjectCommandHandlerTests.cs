using CmScheme.Masters.Application.Features.Projects.CreateProject;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class CreateProjectCommandHandlerTests
{
    private static CreateProjectCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Valid_Command_Creates_Project_And_Returns_NewId()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new CreateProjectCommand
        {
            ProjectName = "Rural Roads",
            ProjectCode = "RR-2026",
            ProjectDescription = "Upgrade rural roads",
            DepartmentName = "PWD",
            StartDate = new DateTime(2026, 1, 1),
            EndDate = new DateTime(2026, 12, 31),
            ProjectIncharge = "A. Kumar",
            BudgetAmount = 500000m,
            BudgetApprovedBy = 10
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.True(result.Value > 0);
        Assert.Equal(1, await ctx.Projects.CountAsync());
        var project = await ctx.Projects.FirstAsync();
        Assert.Equal("Rural Roads", project.ProjectName);
        Assert.Equal("RR-2026", project.ProjectCode);
        Assert.Equal("PWD", project.DepartmentName);
        Assert.Equal(500000m, project.BudgetAmount);
        Assert.Equal(10, project.BudgetApprovedBy);
        Assert.True(project.IsActive);
    }
}