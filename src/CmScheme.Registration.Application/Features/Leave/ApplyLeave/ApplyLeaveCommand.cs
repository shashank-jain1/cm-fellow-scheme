using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Leave.ApplyLeave;

public sealed record ApplyLeaveCommand : ICommand<Result<ApplyLeaveResult>>
{
    public int UserAccountId { get; init; }
    public int LeaveTypeId { get; init; }
    public DateTime FromDate { get; init; }
    public DateTime ToDate { get; init; }
    public bool IsHalfDay { get; init; }
    public string Reason { get; init; } = null!;
    public string? AttachmentPath { get; init; }
}

public sealed record ApplyLeaveResult
{
    public int LeaveApplicationId { get; init; }
    public string ApplicationNumber { get; init; } = null!;
}

public sealed class ApplyLeaveCommandHandler(IRegistrationCommandDbContext dbContext)
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
