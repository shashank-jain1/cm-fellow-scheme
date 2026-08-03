namespace CmScheme.Registration.Core.Entities;

public class TrainingAttendance
{
    public int TrainingAttendanceId { get; set; }
    public int TrainingCompletionId { get; set; }
    public int ApplicantId { get; set; }
    public DateTime AttendanceDate { get; set; }
    public bool IsPresent { get; set; }
    public string? Remarks { get; set; }
    public string? FaceImage { get; set; }
    public decimal? FaceMatchPercentage { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
