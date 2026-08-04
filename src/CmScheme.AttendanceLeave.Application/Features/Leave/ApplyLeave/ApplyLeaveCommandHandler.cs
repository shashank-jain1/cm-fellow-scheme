using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.AttendanceLeave.Core.Entities;
using CmScheme.Common.Core;
using CmScheme.Common.Core.Services;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.ApplyLeave;

public sealed class ApplyLeaveCommandHandler(
    IAttendanceLeaveCommandDbContext dbContext,
    INotificationService notificationService)
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

        var (subject, body, sms) = NotificationTemplates.LeaveApplied(
            request.CreatedBy,
            request.LeaveType,
            request.NumberOfDays,
            $"{request.FromDate:yyyy-MM-dd} to {request.ToDate:yyyy-MM-dd}");

        await notificationService.SendEmailAsync(
            request.ReportingManagerName, // or supervisor email
            subject,
            body,
            cancellationToken);

        return Result<int>.Success(leaveApplication.LeaveApplicationId);
    }
}
