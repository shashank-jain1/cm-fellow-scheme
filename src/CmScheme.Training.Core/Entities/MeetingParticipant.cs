using System.ComponentModel.DataAnnotations;

namespace CmScheme.Training.Core.Entities;

public class MeetingParticipant
{
    [Key]
    public int MeetingParticipantId { get; set; }

    public int TrainingScheduleId { get; set; }

    public int ApplicantId { get; set; }

    [MaxLength(100)]
    public string? ParticipantName { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
