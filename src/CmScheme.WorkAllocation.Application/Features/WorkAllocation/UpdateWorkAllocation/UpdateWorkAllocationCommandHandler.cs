using Ardalis.Result;
using CmScheme.WorkAllocation.Core.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.UpdateWorkAllocation;

public sealed class UpdateWorkAllocationCommandHandler(IWorkAllocationCommandDbContext dbContext)
    : ICommandHandler<UpdateWorkAllocationCommand, Result<bool>>
{
    public async ValueTask<Result<bool>> Handle(UpdateWorkAllocationCommand request, CancellationToken cancellationToken)
    {
        Core.Entities.WorkAllocation? workAllocation = await dbContext.WorkAllocations
            .FirstOrDefaultAsync(w => w.WorkAllocationId == request.WorkAllocationId, cancellationToken);

        if (workAllocation is null)
        {
            return Result<bool>.NotFound("Work allocation not found.");
        }

        workAllocation.ProjectId = request.ProjectId;
        workAllocation.WorkProjectId = request.WorkProjectId;
        workAllocation.WorkDescription = request.WorkDescription;
        workAllocation.Priority = request.Priority;
        workAllocation.StartDate = request.StartDate;
        workAllocation.EndDate = request.EndDate;
        workAllocation.DurationDays = CalculateDurationDays(request.StartDate, request.EndDate);
        workAllocation.SurveysPerIntern = request.SurveysPerIntern;
        workAllocation.DivisionId = request.DivisionId;
        workAllocation.DistrictId = request.DistrictId;
        workAllocation.BlockId = request.BlockId;
        workAllocation.ActiveStatus = request.ActiveStatus;
        workAllocation.Status = request.Status;
        workAllocation.ModifiedOn = DateTime.UtcNow;
        workAllocation.ModifiedBy = request.ModifiedBy;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    /// <summary>
    /// Duration is a derived field: inclusive day count between start and end date,
    /// floored at 1. An open-ended allocation counts as a single day until an end date is set.
    /// </summary>
    private static int CalculateDurationDays(DateTime startDate, DateTime? endDate)
    {
        if (endDate is null)
        {
            return 1;
        }

        int inclusiveDays = (endDate.Value.Date - startDate.Date).Days + 1;
        return Math.Max(1, inclusiveDays);
    }
}
