using Ardalis.Result;
using Mediator;

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
