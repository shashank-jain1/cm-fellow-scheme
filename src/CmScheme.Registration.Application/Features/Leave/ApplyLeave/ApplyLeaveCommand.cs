using Ardalis.Result;
using Mediator;

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
