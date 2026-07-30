using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.GetMonthlyReport;

public sealed record GetMonthlyAttendanceReportQuery : IQuery<Result<MonthlyAttendanceReportResult>>
{
    public int ApplicantId { get; init; }
    public int Month { get; init; }
    public int Year { get; init; }
}

public sealed record MonthlyAttendanceReportResult
{
    public int AttendanceDays { get; init; }
    public decimal TotalHours { get; init; }
    public int AbsentDays { get; init; }
    public int LeaveDays { get; init; }
}
