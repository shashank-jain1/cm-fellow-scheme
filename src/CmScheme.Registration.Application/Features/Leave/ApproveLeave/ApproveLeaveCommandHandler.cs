using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Leave.ApproveLeave;

public sealed class ApproveLeaveCommandHandler(
    IRegistrationCommandDbContext dbContext,
    INotificationService notificationService)
    : ICommandHandler<ApproveLeaveCommand, Result<bool>>
{
    public async ValueTask<Result<bool>> Handle(
        ApproveLeaveCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Action != "Approved" && request.Action != "Rejected")
            return Result<bool>.Invalid(new ValidationError("Action must be 'Approved' or 'Rejected'."));

        LeaveApplication? application = await dbContext.LeaveApplications
            .Include(la => la.UserAccount)
                .ThenInclude(ua => ua!.Applicant)
            .Include(la => la.LeaveType)
            .FirstOrDefaultAsync(la => la.LeaveApplicationId == request.LeaveApplicationId, cancellationToken);

        if (application is null)
            return Result<bool>.NotFound("Leave application not found.");

        if (application.Status != "Pending")
            return Result<bool>.Invalid(new ValidationError($"Leave application is already {application.Status}."));

        Core.Entities.UserAccount? approver = await dbContext.UserAccounts
            .FirstOrDefaultAsync(ua => ua.UserAccountId == request.ApprovedBy, cancellationToken);

        string approverName = approver?.Username ?? "Approver";
        string leaveTypeName = application.LeaveType?.TypeName ?? "Leave";
        string fellowName = application.UserAccount?.Username ?? "Fellow";

        application.Status = request.Action;
        application.ApprovedBy = request.ApprovedBy;
        application.ApprovalRemarks = request.Remarks;
        application.ApprovalDate = DateTime.UtcNow;

        if (request.Action == "Approved")
        {
            int currentYear = application.FromDate.Year;

            LeaveBalance? balance = await dbContext.LeaveBalances
                .FirstOrDefaultAsync(lb =>
                    lb.UserAccountId == application.UserAccountId &&
                    lb.LeaveTypeId == application.LeaveTypeId &&
                    lb.Year == currentYear, cancellationToken);

            if (balance is null)
                return Result<bool>.NotFound("Leave balance record not found.");

            balance.UsedDays += application.NumberOfDays;
            balance.RemainingDays -= application.NumberOfDays;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        string subject = $"Leave {request.Action}";
        string body = $"Dear {fellowName},\n\n" +
            $"Your {leaveTypeName} leave from {application.FromDate:dd/MM/yyyy} to {application.ToDate:dd/MM/yyyy} " +
            $"has been {request.Action.ToLower()} by {approverName}.\n\n" +
            $"Remarks: {request.Remarks ?? "None"}\n\n" +
            $"Thank you.";

        string? fellowEmail = application.UserAccount?.Applicant?.EmailId;
        if (!string.IsNullOrWhiteSpace(fellowEmail))
        {
            await notificationService.SendEmailAsync(fellowEmail, subject, body, cancellationToken);
        }

        return Result<bool>.Success(true);
    }
}
