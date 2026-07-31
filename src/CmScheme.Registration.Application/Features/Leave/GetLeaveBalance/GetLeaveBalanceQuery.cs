using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Leave.GetLeaveBalance;

public sealed record GetLeaveBalanceQuery : IQuery<Result<List<LeaveBalanceResult>>>
{
    public int UserAccountId { get; init; }
    public int Year { get; init; }
}

public sealed record LeaveBalanceResult
{
    public int LeaveBalanceId { get; init; }
    public string LeaveTypeName { get; init; } = null!;
    public string LeaveTypeCode { get; init; } = null!;
    public decimal TotalDays { get; init; }
    public decimal UsedDays { get; init; }
    public decimal RemainingDays { get; init; }
}
