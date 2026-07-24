using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.AttendanceLeave.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.ApproveLeave;

public sealed class ApproveLeaveCommandHandler(IAttendanceLeaveCommandDbContext dbContext)
    : ICommandHandler<ApproveLeaveCommand, Result<bool>>
{
    public async ValueTask<Result<bool>> Handle(ApproveLeaveCommand request, CancellationToken cancellationToken)
    {
        LeaveApplication? leaveApplication = await dbContext.LeaveApplications
            .FirstOrDefaultAsync(l => l.LeaveApplicationId == request.LeaveApplicationId, cancellationToken);

        if (leaveApplication is null)
        {
            return Result<bool>.NotFound("Leave application not found.");
        }

        leaveApplication.Status = request.Status;
        leaveApplication.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
