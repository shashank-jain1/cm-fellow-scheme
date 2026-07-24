using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.GetAttendanceHistory;

public sealed record GetAttendanceHistoryQuery(int ApplicantId, DateTime FromDate, DateTime ToDate) : IQuery<Result<IReadOnlyList<Core.Dtos.AttendanceDto>>>;
