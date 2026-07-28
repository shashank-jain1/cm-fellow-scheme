using System.ComponentModel.DataAnnotations;

namespace CmScheme.Masters.Core.Entities;

public class TrainingSchedule
{
    [Key]
    public int TrainingScheduleId { get; set; }

    [Required]
    [MaxLength(20)]
    public string CalendarYear { get; set; } = null!;

    public int ProjectId { get; set; }

    public int? WorkId { get; set; }

    public int? DivisionId { get; set; }

    public int? DistrictId { get; set; }

    public int? BlockId { get; set; }

    public DateTime TrainingDate { get; set; }

    [MaxLength(250)]
    public string? VenueName { get; set; }

    [MaxLength(1000)]
    public string? TrainingDescription { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;

    public int? ModifiedBy { get; set; }
}
