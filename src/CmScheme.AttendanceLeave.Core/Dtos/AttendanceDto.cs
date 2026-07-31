namespace CmScheme.AttendanceLeave.Core.Dtos;

public sealed class AttendanceDto
{
    public int AttendanceId { get; init; }
    public int ApplicantId { get; init; }
    public DateTime AttendanceDate { get; init; }
    public TimeOnly CheckInTime { get; init; }
    public TimeOnly? CheckOutTime { get; init; }
    public string CaptureFacePath { get; init; } = null!;
    public decimal? FaceMatchPercentage { get; init; }
    public string FaceVerificationStatus { get; init; } = null!;
    public decimal? Latitude { get; init; }
    public decimal? Longitude { get; init; }
    public string AttendanceStatus { get; init; } = null!;
    public DateTime CreatedOn { get; init; }
}
