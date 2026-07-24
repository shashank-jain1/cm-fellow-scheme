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
            DurationDays = request.DurationDays,
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
}
