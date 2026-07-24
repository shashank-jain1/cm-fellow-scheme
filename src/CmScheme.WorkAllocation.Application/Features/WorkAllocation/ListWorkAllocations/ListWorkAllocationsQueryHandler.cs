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
                CreatedOn = w.CreatedOn,
                CreatedBy = w.CreatedBy,
                ModifiedOn = w.ModifiedOn,
                ModifiedBy = w.ModifiedBy
            })
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<WorkAllocationDto>>.Success(workAllocations);
    }
}
