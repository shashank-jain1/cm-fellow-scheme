using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.GetLeaveStatus;

public sealed record GetLeaveStatusQuery : IQuery<Result<IReadOnlyList<Core.Dtos.LeaveStatusDto>>>
{
    public int? ApplicantId { get; init; }
}
