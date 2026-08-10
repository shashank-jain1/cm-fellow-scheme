using Ardalis.Result;
using CmScheme.Masters.Application.Features.Projects.UpdateProject;
using CmScheme.Masters.Core.Entities;
using CmScheme.Masters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Tests.Masters.Handlers;

public class UpdateProjectCommandHandlerTests
{
    private static UpdateProjectCommandHandler CreateHandler(MastersCommandDbContext ctx) => new(ctx);

    [Fact]
    public async Task Handle_Existing_Project_Updates_Fields_And_Returns_NoContent()
    {
        var ctx = TestDbContext.CreateMasters(c =>
        {
            c.Projects.Add(new Project
            {
                ProjectName = "Old",
                ProjectCode = "OL",
                DepartmentName = "PWD",
                ProjectIncharge = "Person",
                BudgetApprovedBy = 1,
                IsActive = true
            });
            c.SaveChanges();
        });
        var handler = CreateHandler(ctx);
        var projectId = await ctx.Projects.Select(p => p.ProjectId).FirstAsync();

        var result = await handler.Handle(new UpdateProjectCommand
        {
            ProjectId = projectId,
            ProjectName = "New",
            ProjectCode = "NW",
            ProjectDescription = "Desc",
            DepartmentName = "PWD",
            StartDate = new DateTime(2026, 1, 1),
            EndDate = new DateTime(2026, 12, 31),
            ProjectIncharge = "Person",
            BudgetAmount = 1000m,
            BudgetApprovedBy = 1,
            IsActive = false
        }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.NoContent, result.Status);
        var updated = await ctx.Projects.FirstAsync(p => p.ProjectId == projectId);
        Assert.Equal("New", updated.ProjectName);
        Assert.Equal("NW", updated.ProjectCode);
        Assert.Equal(1000m, updated.BudgetAmount);
        Assert.False(updated.IsActive);
    }

    [Fact]
    public async Task Handle_Missing_Project_Returns_NotFound()
    {
        var ctx = TestDbContext.CreateMasters();
        var handler = CreateHandler(ctx);

        var result = await handler.Handle(new UpdateProjectCommand
        {
            ProjectId = 999,
            ProjectName = "X",
            ProjectCode = "X",
            DepartmentName = "X",
            ProjectIncharge = "X",
            BudgetApprovedBy = 0,
            IsActive = true
        }, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}