using System.ComponentModel.DataAnnotations;

namespace CmScheme.Performance.Core.Entities;

public class PerformanceGoal
{
    [Key]
    public int PerformanceGoalId { get; set; }
    public int UserAccountId { get; set; }
    [Required]
    [MaxLength(200)]
    public string GoalTitle { get; set; } = null!;
    [MaxLength(1000)]
    public string? Description { get; set; }
    public DateTime TargetDate { get; set; }
    [MaxLength(20)]
    public string Status { get; set; } = "NotStarted";
    public int? ReviewCycleId { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
