using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.AttendanceLeave.Core.Entities;
using CmScheme.Common.Core;
using CmScheme.Common.Core.Services;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.ApproveLeave;

public sealed class ApproveLeaveCommandHandler(
    IAttendanceLeaveCommandDbContext dbContext,
    INotificationService notificationService)
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

        if (request.Status == Statuses.Leave.Approved)
        {
            LeaveBalance? leaveBalance = await dbContext.LeaveBalances
                .FirstOrDefaultAsync(
                    lb => lb.ApplicantId == leaveApplication.ApplicantId
                       && lb.LeaveType == leaveApplication.LeaveType,
                    cancellationToken);

            int numberOfDays = (int)leaveApplication.NumberOfDays;

            if (leaveBalance is not null)
            {
                leaveBalance.AvailableBalance -= numberOfDays;
                leaveBalance.AvailedLeave += numberOfDays;
            }
            else
            {
                LeaveBalance newBalance = new LeaveBalance
                {
                    ApplicantId = leaveApplication.ApplicantId,
                    LeaveType = leaveApplication.LeaveType,
                    OpeningBalance = 0,
                    AvailedLeave = numberOfDays,
                    PendingApprovalLeave = 0,
                    AvailableBalance = -numberOfDays
                };
                dbContext.LeaveBalances.Add(newBalance);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        if (request.Status == Statuses.Leave.Approved)
        {
            await notificationService.SendEmailAsync(
                leaveApplication.CreatedBy,
                "Leave Application Approved",
                $"Dear Fellow,\n\n" +
                $"Your leave application ({leaveApplication.LeaveType}) for {leaveApplication.NumberOfDays} day(s) has been approved.\n\n" +
                $"Best regards,\nCM Fellow Program Team",
                cancellationToken);
        }
        else if (request.Status == Statuses.Leave.Rejected)
        {
            await notificationService.SendEmailAsync(
                leaveApplication.CreatedBy,
                "Leave Application Rejected",
                $"Dear Fellow,\n\n" +
                $"Your leave application ({leaveApplication.LeaveType}) for {leaveApplication.NumberOfDays} day(s) has been rejected.\n\n" +
                $"Best regards,\nCM Fellow Program Team",
                cancellationToken);
        }

        return Result<bool>.Success(true);
    }
}
