using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Application.Features.Leave.GetLeaveStatus;

public sealed record GetLeaveStatusQuery : IQuery<Result<List<LeaveStatusResult>>>
{
    public int UserAccountId { get; init; }
}

public sealed record LeaveStatusResult
{
    public int LeaveApplicationId { get; init; }
    public string ApplicationNumber { get; init; } = null!;
    public string LeaveTypeName { get; init; } = null!;
    public DateTime FromDate { get; init; }
    public DateTime ToDate { get; init; }
    public decimal NumberOfDays { get; init; }
    public bool IsHalfDay { get; init; }
    public string Reason { get; init; } = null!;
    public string Status { get; init; } = null!;
    public string? ApprovalRemarks { get; init; }
    public DateTime? ApprovalDate { get; init; }
    public DateTime CreatedOn { get; init; }
}

public sealed class GetLeaveStatusQueryHandler(IRegistrationCommandDbContext dbContext)
    : IQueryHandler<GetLeaveStatusQuery, Result<List<LeaveStatusResult>>>
{
    public async ValueTask<Result<List<LeaveStatusResult>>> Handle(
        GetLeaveStatusQuery request,
        CancellationToken cancellationToken)
    {
        List<LeaveStatusResult> result = await dbContext.LeaveApplications
            .Where(la => la.UserAccountId == request.UserAccountId)
            .OrderByDescending(la => la.CreatedOn)
            .Select(la => new LeaveStatusResult
            {
                LeaveApplicationId = la.LeaveApplicationId,
                ApplicationNumber = la.ApplicationNumber,
                LeaveTypeName = la.LeaveType.TypeName,
                FromDate = la.FromDate,
                ToDate = la.ToDate,
                NumberOfDays = la.NumberOfDays,
                IsHalfDay = la.IsHalfDay,
                Reason = la.Reason,
                Status = la.Status,
                ApprovalRemarks = la.ApprovalRemarks,
                ApprovalDate = la.ApprovalDate,
                CreatedOn = la.CreatedOn,
            })
            .ToListAsync(cancellationToken);

        return Result<List<LeaveStatusResult>>.Success(result);
    }
}
