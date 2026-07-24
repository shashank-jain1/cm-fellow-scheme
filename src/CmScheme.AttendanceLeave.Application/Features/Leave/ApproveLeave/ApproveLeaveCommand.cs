using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.ApproveLeave;

public sealed record ApproveLeaveCommand(int LeaveApplicationId, string Status, string Remarks) : ICommand<Result<bool>>;
