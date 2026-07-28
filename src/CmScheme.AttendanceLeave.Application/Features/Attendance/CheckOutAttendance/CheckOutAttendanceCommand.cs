using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.CheckOutAttendance;

public sealed record CheckOutAttendanceCommand : ICommand<Result>
{
    public int ApplicantId { get; init; }
    public DateTime AttendanceDate { get; init; }
}
