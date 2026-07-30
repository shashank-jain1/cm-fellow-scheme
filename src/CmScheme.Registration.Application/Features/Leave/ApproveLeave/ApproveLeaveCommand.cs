using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Leave.ApproveLeave;

public sealed record ApproveLeaveCommand : ICommand<Result<bool>>
{
    public int LeaveApplicationId { get; init; }
    public int ApprovedBy { get; init; }
    public string Action { get; init; } = null!;
    public string? Remarks { get; init; }
}

public sealed class ApproveLeaveCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<ApproveLeaveCommand, Result<bool>>
{
    public async ValueTask<Result<bool>> Handle(
        ApproveLeaveCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Action != "Approved" && request.Action != "Rejected")
            return Result<bool>.Invalid(new ValidationError("Action must be 'Approved' or 'Rejected'."));

        LeaveApplication? application = await dbContext.LeaveApplications
            .FirstOrDefaultAsync(la => la.LeaveApplicationId == request.LeaveApplicationId, cancellationToken);

        if (application is null)
            return Result<bool>.NotFound("Leave application not found.");

        if (application.Status != "Pending")
            return Result<bool>.Invalid(new ValidationError($"Leave application is already {application.Status}."));

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

        return Result<bool>.Success(true);
    }
}
