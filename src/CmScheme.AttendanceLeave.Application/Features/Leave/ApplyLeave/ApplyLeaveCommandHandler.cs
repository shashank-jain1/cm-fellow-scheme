using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.AttendanceLeave.Core.Entities;
using CmScheme.Common.Core;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.ApplyLeave;

public sealed class ApplyLeaveCommandHandler(IAttendanceLeaveCommandDbContext dbContext)
    : ICommandHandler<ApplyLeaveCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(ApplyLeaveCommand request, CancellationToken cancellationToken)
    {
        LeaveApplication leaveApplication = new LeaveApplication
        {
            ApplicantId = request.ApplicantId,
            LeaveType = request.LeaveType,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            NumberOfDays = request.NumberOfDays,
            HalfDayFullDay = request.HalfDayFullDay,
            LeaveReason = request.LeaveReason,
            AttachmentPath = request.AttachmentPath,
            ReportingManagerName = request.ReportingManagerName,
            Status = Statuses.Leave.Pending,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = request.CreatedBy
        };

        dbContext.LeaveApplications.Add(leaveApplication);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(leaveApplication.LeaveApplicationId);
    }
}
