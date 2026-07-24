using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.GetLeaveStatus;

public sealed record GetLeaveStatusQuery(int ApplicantId) : IQuery<Result<IReadOnlyList<Core.Dtos.LeaveStatusDto>>>;
