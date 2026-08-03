namespace CmScheme.AttendanceLeave.Core.Dtos;

public sealed class WeeklyAttendanceReportDto
{
    public int ApplicantId { get; init; }
    public DateTime WeekStartDate { get; init; }
    public DateTime WeekEndDate { get; init; }
    public int TotalAttendanceDays { get; init; }
    public decimal TotalHours { get; init; }
    public List<DailyAttendanceRecordDto> DailyRecords { get; init; } = [];
}

public sealed class DailyAttendanceRecordDto
{
    public DateTime AttendanceDate { get; init; }
    public TimeOnly CheckInTime { get; init; }
    public TimeOnly? CheckOutTime { get; init; }
    public decimal? Latitude { get; init; }
    public decimal? Longitude { get; init; }
    public string AttendanceStatus { get; init; } = null!;
    public decimal HoursWorked { get; init; }
}
