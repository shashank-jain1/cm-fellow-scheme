using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.MarkAttendance;

public sealed record MarkAttendanceCommand : ICommand<Result<int>>
{
    public int ApplicantId { get; init; }
    public DateTime AttendanceDate { get; init; }
    public TimeOnly CheckInTime { get; init; }
    public TimeOnly? CheckOutTime { get; init; }
    public string CaptureFacePath { get; init; } = string.Empty;
    public decimal? FaceMatchPercentage { get; init; }
    public string FaceVerificationStatus { get; init; } = string.Empty;
    public decimal? Latitude { get; init; }
    public decimal? Longitude { get; init; }
    public string AttendanceStatus { get; init; } = string.Empty;
}
