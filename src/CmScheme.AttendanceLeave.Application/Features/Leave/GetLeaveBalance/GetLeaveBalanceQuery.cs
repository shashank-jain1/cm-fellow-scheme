using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.GetLeaveBalance;

public sealed record GetLeaveBalanceQuery : IQuery<Result<IReadOnlyList<Core.Dtos.LeaveBalanceDto>>>
{
    public int ApplicantId { get; init; }
}
