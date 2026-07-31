using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Leave.ApproveLeave;

public sealed record ApproveLeaveCommand : ICommand<Result<bool>>
{
    public int LeaveApplicationId { get; init; }
    public int ApprovedBy { get; init; }
    public string Action { get; init; } = null!;
    public string? Remarks { get; init; }
}
