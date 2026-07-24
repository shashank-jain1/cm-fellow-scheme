using System.ComponentModel.DataAnnotations;

namespace CmScheme.Training.Core.Entities;

public class TrainingParticipant
{
    [Key]
    public int TrainingParticipantId { get; set; }
    public int TrainingScheduleId { get; set; }
    public int ParticipantUserId { get; set; }
    [MaxLength(20)]
    public string Status { get; set; } = "Invited";
}
