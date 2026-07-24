using Ardalis.Result;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.GetWorkAllocationById;

public sealed class GetWorkAllocationByIdQueryHandler(IWorkAllocationQueryDbContext dbContext)
    : IQueryHandler<GetWorkAllocationByIdQuery, Result<WorkAllocationDto?>>
{
    public async ValueTask<Result<WorkAllocationDto?>> Handle(GetWorkAllocationByIdQuery request, CancellationToken cancellationToken)
    {
        WorkAllocationDto? workAllocation = await dbContext.WorkAllocations
            .Where(w => w.WorkAllocationId == request.WorkAllocationId)
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
            .FirstOrDefaultAsync(cancellationToken);

        return Result<WorkAllocationDto?>.Success(workAllocation);
    }
}
