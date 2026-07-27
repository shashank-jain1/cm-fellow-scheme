using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.GetAttendanceHistory;

public sealed record GetAttendanceHistoryQuery : IQuery<Result<IReadOnlyList<Core.Dtos.AttendanceDto>>>
{
    public int? ApplicantId { get; init; }
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }
}
