namespace CmScheme.AttendanceLeave.Core.Entities;

public class Attendance
{
    public int AttendanceId { get; set; }
    public int ApplicantId { get; set; }
    public DateTime AttendanceDate { get; set; }
    public TimeOnly CheckInTime { get; set; }
    public TimeOnly? CheckOutTime { get; set; }
    public string CaptureFacePath { get; set; } = null!;
    public decimal? FaceMatchPercentage { get; set; }
    public string FaceVerificationStatus { get; set; } = null!;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string AttendanceStatus { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
}
