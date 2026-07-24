using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Dtos;

namespace CmScheme.Masters.Application.Features.Projects.ListProjects;

public sealed class ListProjectsQueryHandler(IMastersQueryDbContext dbContext)
    : IQueryHandler<ListProjectsQuery, Result<List<ProjectDto>>>
{
    public async ValueTask<Result<List<ProjectDto>>> Handle(
        ListProjectsQuery request,
        CancellationToken cancellationToken)
    {
        List<ProjectDto> projects = await dbContext.Projects
            .Where(p => p.IsActive)
            .OrderByDescending(p => p.CreatedOn)
            .Select(p => new ProjectDto(
                p.ProjectId,
                p.ProjectName,
                p.ProjectCode,
                p.ProjectDescription,
                p.DepartmentName,
                p.StartDate,
                p.EndDate,
                p.ProjectIncharge,
                p.BudgetAmount,
                p.IsActive))
            .ToListAsync(cancellationToken);

        return Result<List<ProjectDto>>.Success(projects);
    }
}
