using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.MarkAttendance;

public sealed record MarkAttendanceCommand(
    int ApplicantId,
    DateTime AttendanceDate,
    TimeOnly CheckInTime,
    TimeOnly? CheckOutTime,
    string CaptureFacePath,
    decimal? FaceMatchPercentage,
    string FaceVerificationStatus,
    decimal? Latitude,
    decimal? Longitude,
    string AttendanceStatus
) : ICommand<Result<int>>;
