using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Leave.ApplyLeave;

public sealed class ApplyLeaveCommandHandler(
    IRegistrationCommandDbContext dbContext,
    INotificationService notificationService)
    : ICommandHandler<ApplyLeaveCommand, Result<ApplyLeaveResult>>
{
    public async ValueTask<Result<ApplyLeaveResult>> Handle(
        ApplyLeaveCommand request,
        CancellationToken cancellationToken)
    {
        if (request.FromDate > request.ToDate)
            return Result<ApplyLeaveResult>.Invalid(new ValidationError("FromDate must be before or equal to ToDate."));

        if (request.FromDate.Date < DateTime.UtcNow.Date)
            return Result<ApplyLeaveResult>.Invalid(new ValidationError("FromDate cannot be in the past."));

        LeaveType? leaveType = await dbContext.LeaveTypes
            .FirstOrDefaultAsync(lt => lt.LeaveTypeId == request.LeaveTypeId && lt.IsActive, cancellationToken);

        if (leaveType is null)
            return Result<ApplyLeaveResult>.NotFound("Leave type not found.");

        decimal numberOfDays = CalculateDays(request);

        int currentYear = request.FromDate.Year;
        LeaveBalance? balance = await dbContext.LeaveBalances
            .FirstOrDefaultAsync(lb =>
                lb.UserAccountId == request.UserAccountId &&
                lb.LeaveTypeId == request.LeaveTypeId &&
                lb.Year == currentYear, cancellationToken);

        if (balance is null)
            return Result<ApplyLeaveResult>.Invalid(new ValidationError("No leave balance found for this leave type and year."));

        if (balance.RemainingDays < numberOfDays)
            return Result<ApplyLeaveResult>.Invalid(new ValidationError($"Insufficient leave balance. Remaining: {balance.RemainingDays}, Requested: {numberOfDays}."));

        bool hasConflict = await dbContext.LeaveApplications
            .AnyAsync(la =>
                la.UserAccountId == request.UserAccountId &&
                la.Status != "Cancelled" &&
                la.Status != "Rejected" &&
                la.FromDate <= request.ToDate &&
                la.ToDate >= request.FromDate,
                cancellationToken);

        if (hasConflict)
            return Result<ApplyLeaveResult>.Invalid(new ValidationError("Leave overlaps with an existing approved or pending leave application."));

        string applicationNumber = await GenerateApplicationNumberAsync(cancellationToken);

        LeaveApplication application = new()
        {
            ApplicationNumber = applicationNumber,
            UserAccountId = request.UserAccountId,
            LeaveTypeId = request.LeaveTypeId,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            NumberOfDays = numberOfDays,
            IsHalfDay = request.IsHalfDay,
            Reason = request.Reason,
            AttachmentPath = request.AttachmentPath,
            Status = "Pending",
            CreatedOn = DateTime.UtcNow,
            CreatedBy = request.UserAccountId,
        };

        dbContext.LeaveApplications.Add(application);
        await dbContext.SaveChangesAsync(cancellationToken);

        LeaveType? appliedLeaveType = await dbContext.LeaveTypes
            .FirstOrDefaultAsync(lt => lt.LeaveTypeId == request.LeaveTypeId, cancellationToken);

        string leaveTypeName = appliedLeaveType?.TypeName ?? "Leave";

        List<Core.Entities.UserAccount> coordinators = await dbContext.UserAccounts
            .Include(ua => ua.Applicant)
            .Where(ua => ua.Role == Statuses.ReviewLevel.Coordinator && ua.IsActive)
            .ToListAsync(cancellationToken);

        string subject = "New Leave Application Submitted";
        string body = $"A new {leaveTypeName} leave application ({application.ApplicationNumber}) " +
            $"has been submitted by User #{application.UserAccountId} " +
            $"from {application.FromDate:dd/MM/yyyy} to {application.ToDate:dd/MM/yyyy} " +
            $"({application.NumberOfDays} day(s)).\n\n" +
            $"Please review and approve/reject this application.";

        foreach (Core.Entities.UserAccount coordinator in coordinators)
        {
            string? email = coordinator.Applicant?.EmailId;
            if (!string.IsNullOrWhiteSpace(email))
            {
                await notificationService.SendEmailAsync(email, subject, body, cancellationToken);
            }
        }

        return Result<ApplyLeaveResult>.Success(new ApplyLeaveResult
        {
            LeaveApplicationId = application.LeaveApplicationId,
            ApplicationNumber = application.ApplicationNumber,
        });
    }

    private static decimal CalculateDays(ApplyLeaveCommand request)
    {
        decimal days = (decimal)(request.ToDate.Date - request.FromDate.Date).Days + 1;
        if (request.IsHalfDay && days == 1)
            return 0.5m;
        return days;
    }

    private async Task<string> GenerateApplicationNumberAsync(CancellationToken cancellationToken)
    {
        string prefix = DateTime.UtcNow.ToString("yyyyMM");
        string pattern = $"LV{prefix}%";

        int count = await dbContext.LeaveApplications
            .CountAsync(la => la.ApplicationNumber.StartsWith($"LV{prefix}"), cancellationToken);

        int sequence = count + 1;
        return $"LV{prefix}{sequence:D4}";
    }
}
