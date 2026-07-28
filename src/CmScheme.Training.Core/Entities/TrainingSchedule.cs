using System.ComponentModel.DataAnnotations;

namespace CmScheme.Training.Core.Entities;

public class TrainingSchedule
{
    [Key]
    public int TrainingScheduleId { get; set; }
    [Required]
    [MaxLength(20)]
    public string ActivityType { get; set; } = null!;
    public int ProjectId { get; set; }
    public int WorkProjectId { get; set; }
    [MaxLength(250)]
    public string? ActivityTitle { get; set; }
    [MaxLength(250)]
    public string? TrainingTitle { get; set; }
    [MaxLength(250)]
    public string? MeetingTitle { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    [MaxLength(50)]
    public string? Mode { get; set; }
    [MaxLength(2000)]
    public string? Remarks { get; set; }
    [MaxLength(255)]
    public string? MaterialPath { get; set; }
    [MaxLength(255)]
    public string? AttachmentPath { get; set; }
    [MaxLength(20)]
    public string Status { get; set; } = "Scheduled";
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public int? CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    public int? ModifiedBy { get; set; }

    // Training-specific fields
    [MaxLength(250)]
    public string? TrainingCategory { get; set; }
    [MaxLength(2000)]
    public string? TrainingDescription { get; set; }
    [MaxLength(500)]
    public string? TargetUserTypes { get; set; }
    [MaxLength(100)]
    public string? TrainerName { get; set; }
    [MaxLength(10)]
    public string? TrainerMobile { get; set; }
    public bool AttendanceRequired { get; set; }

    // Meeting-specific fields
    [MaxLength(2000)]
    public string? MeetingAgenda { get; set; }
    [MaxLength(2000)]
    public string? MeetingDescription { get; set; }
    public int? ConductPersonId { get; set; }
    public int? CoordinatorId { get; set; }
    public bool MOMRequired { get; set; }

    // Location multi-select (comma-separated IDs)
    [MaxLength(1000)]
    public string? ApplicableDivisionIds { get; set; }
    [MaxLength(1000)]
    public string? ApplicableDistrictIds { get; set; }
    [MaxLength(1000)]
    public string? ApplicableBlockIds { get; set; }
}
