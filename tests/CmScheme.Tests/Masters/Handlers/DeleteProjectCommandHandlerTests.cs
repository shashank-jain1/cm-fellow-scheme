using Ardalis.Result;
using CmScheme.Masters.Application.Features.Projects.DeleteProject;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class DeleteProjectCommandHandlerTests
{
    private static DeleteProjectCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_Project_Deactivates_And_Returns_NoContent()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.Projects.Add(new Project
            {
                ProjectName = "Rural Roads",
                ProjectCode = "RR",
                DepartmentName = "PWD",
                ProjectIncharge = "Person",
                BudgetApprovedBy = 1,
                IsActive = true
            });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var projectId = await ctx.Projects.Select(p => p.ProjectId).FirstAsync();

        var result = await handler.Handle(new DeleteProjectCommand { ProjectId = projectId }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var project = await ctx.Projects.FirstAsync();
        Assert.False(project.IsActive);
    }

    [Fact]
    public async Task Handle_Missing_Project_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new DeleteProjectCommand { ProjectId = 999 }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}