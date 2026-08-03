using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Dtos;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.GetWeeklyReport;

public sealed record GetWeeklyAttendanceReportQuery : IQuery<Result<WeeklyAttendanceReportDto>>
{
    public int ApplicantId { get; init; }
    public DateTime WeekStartDate { get; init; }
}
