using System.ComponentModel.DataAnnotations;

namespace CmScheme.Registration.Core.Entities;

public class TrainingEnrollment
{
    [Key]
    public int TrainingEnrollmentId { get; set; }

    public int TrainingScheduleId { get; set; }

    public int UserAccountId { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Enrolled";

    public bool AttendanceMarked { get; set; }

    public bool CertificateIssued { get; set; }

    public DateTime EnrolledOn { get; set; }

    public DateTime? CompletedOn { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
