using System.ComponentModel.DataAnnotations;

namespace CmScheme.Training.Core.Entities;

public class TrainingCompletion
{
    [Key]
    public int TrainingCompletionId { get; set; }
    public int TrainingScheduleId { get; set; }
    public int UserAccountId { get; set; }
    [MaxLength(20)]
    public string Status { get; set; } = "InProgress";
    public DateTime? CompletedOn { get; set; }
    public bool CertificateIssued { get; set; }
    public int? FeedbackRating { get; set; }
    [MaxLength(2000)]
    public string? FeedbackComments { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
