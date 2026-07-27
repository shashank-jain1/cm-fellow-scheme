using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.ApproveLeave;

public sealed record ApproveLeaveCommand : ICommand<Result<bool>>
{
    public int LeaveApplicationId { get; init; }
    public string Status { get; init; } = string.Empty;
    public string Remarks { get; init; } = string.Empty;
}
