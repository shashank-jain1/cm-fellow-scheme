using Ardalis.Result;
using CmScheme.Dashboard.Core.Dtos;
using CmScheme.WorkAllocation.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Dashboard.Application.Features.Dashboard.GetProjectProgress;

public sealed class GetProjectProgressQueryHandler(IWorkAllocationQueryDbContext dbContext)
    : IQueryHandler<GetProjectProgressQuery, Result<List<ProjectProgressDto>>>
{
    public async ValueTask<Result<List<ProjectProgressDto>>> Handle(
        GetProjectProgressQuery request,
        CancellationToken cancellationToken)
    {
        List<ProjectProgressDto> progress = await dbContext.TaskProgresses
            .GroupBy(t => t.ProjectName)
            .Select(g => new ProjectProgressDto(
                g.Key,
                g.Average(t => t.CompletionPercentage),
                g.Sum(t => t.NumberOfSurveys),
                g.Sum(t => t.CompletedSurveys)))
            .ToListAsync(cancellationToken);

        return Result<List<ProjectProgressDto>>.Success(progress);
    }
}
