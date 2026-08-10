using Ardalis.Result;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.ListWorkAllocations;

public sealed class ListWorkAllocationsQueryHandler(IWorkAllocationQueryDbContext dbContext)
    : IQueryHandler<ListWorkAllocationsQuery, Result<IReadOnlyList<WorkAllocationDto>>>
{
    public async ValueTask<Result<IReadOnlyList<WorkAllocationDto>>> Handle(ListWorkAllocationsQuery request, CancellationToken cancellationToken)
    {
        // Progress is derived per allocation from its task rows, matching the
        // "(Completed Surveys / Assigned Surveys) x 100" rule in the module spec.
        var progressTotals = await dbContext.TaskProgresses
            .GroupBy(tp => tp.WorkAllocationId)
            .Select(g => new
            {
                WorkAllocationId = g.Key,
                Assigned = g.Sum(tp => tp.NumberOfSurveys),
                Completed = g.Sum(tp => tp.CompletedSurveys),
            })
            .ToListAsync(cancellationToken);

        Dictionary<int, decimal> completionByAllocation = progressTotals.ToDictionary(
            t => t.WorkAllocationId,
            t => t.Assigned > 0
                ? Math.Round((decimal)t.Completed / t.Assigned * 100m, 2)
                : 0m);

        List<WorkAllocationDto> workAllocations = await dbContext.WorkAllocations
            .Select(w => new WorkAllocationDto
            {
                WorkAllocationId = w.WorkAllocationId,
                ProjectId = w.ProjectId,
                WorkProjectId = w.WorkProjectId,
                WorkDescription = w.WorkDescription,
                Priority = w.Priority,
                StartDate = w.StartDate,
                EndDate = w.EndDate,
                DurationDays = w.DurationDays,
                SurveysPerIntern = w.SurveysPerIntern,
                DivisionId = w.DivisionId,
                DistrictId = w.DistrictId,
                BlockId = w.BlockId,
                ActiveStatus = w.ActiveStatus,
                Status = w.Status,
                AssignedToUserId = w.AssignedToUserId,
                CreatedOn = w.CreatedOn,
                CreatedBy = w.CreatedBy,
                ModifiedOn = w.ModifiedOn,
                ModifiedBy = w.ModifiedBy
            })
            .ToListAsync(cancellationToken);

        foreach (WorkAllocationDto dto in workAllocations)
        {
            dto.CompletionPercentage = completionByAllocation.GetValueOrDefault(dto.WorkAllocationId);
        }

        return Result<IReadOnlyList<WorkAllocationDto>>.Success(workAllocations);
    }
}
