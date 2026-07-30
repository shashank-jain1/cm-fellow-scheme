using System.ComponentModel.DataAnnotations;

namespace CmScheme.Performance.Core.Entities;

public class SelfAssessment
{
    [Key]
    public int SelfAssessmentId { get; set; }
    public int UserAccountId { get; set; }
    public int? ReviewCycleId { get; set; }
    [MaxLength(1000)]
    public string? Strengths { get; set; }
    [MaxLength(1000)]
    public string? Improvements { get; set; }
    [MaxLength(1000)]
    public string? GoalsAchieved { get; set; }
    [MaxLength(1000)]
    public string? GoalsMissed { get; set; }
    [MaxLength(1000)]
    public string? TrainingFeedback { get; set; }
    public int OverallRating { get; set; }
    public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;
    [MaxLength(20)]
    public string Status { get; set; } = "Draft";
}
