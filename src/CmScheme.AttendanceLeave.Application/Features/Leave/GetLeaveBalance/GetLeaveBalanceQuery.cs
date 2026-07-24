using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.GetLeaveBalance;

public sealed record GetLeaveBalanceQuery(int ApplicantId) : IQuery<Result<IReadOnlyList<Core.Dtos.LeaveBalanceDto>>>;
