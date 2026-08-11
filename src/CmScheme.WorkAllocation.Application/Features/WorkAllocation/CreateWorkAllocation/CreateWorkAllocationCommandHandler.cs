using Ardalis.Result;
using CmScheme.WorkAllocation.Core.Data;
using CmScheme.WorkAllocation.Core.Entities;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.CreateWorkAllocation;

public sealed class CreateWorkAllocationCommandHandler(IWorkAllocationCommandDbContext dbContext)
    : ICommandHandler<CreateWorkAllocationCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(CreateWorkAllocationCommand request, CancellationToken cancellationToken)
    {
        Core.Entities.WorkAllocation workAllocation = new Core.Entities.WorkAllocation
        {
            ProjectId = request.ProjectId,
            WorkProjectId = request.WorkProjectId,
            WorkDescription = request.WorkDescription,
            Priority = request.Priority,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            DurationDays = CalculateDurationDays(request.StartDate, request.EndDate),
            SurveysPerIntern = request.SurveysPerIntern,
            DivisionId = request.DivisionId,
            DistrictId = request.DistrictId,
            BlockId = request.BlockId,
            ActiveStatus = request.ActiveStatus,
            Status = request.Status,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = request.CreatedBy
        };

        dbContext.WorkAllocations.Add(workAllocation);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(workAllocation.WorkAllocationId);
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
