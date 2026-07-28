using System.ComponentModel.DataAnnotations;

namespace CmScheme.Performance.Core.Entities;

public class PerformanceReviewHistory
{
    [Key]
    public int PerformanceReviewHistoryId { get; set; }
    public int PerformanceEvaluationId { get; set; }
    [MaxLength(20)]
    public string Action { get; set; } = null!;
    [MaxLength(20)]
    public string PreviousLevel { get; set; } = null!;
    [MaxLength(20)]
    public string NewLevel { get; set; } = null!;
    [MaxLength(20)]
    public string PreviousStatus { get; set; } = null!;
    [MaxLength(20)]
    public string NewStatus { get; set; } = null!;
    [MaxLength(200)]
    public string? PerformedBy { get; set; }
    [MaxLength(2000)]
    public string? Remarks { get; set; }
    public DateTime PerformedOn { get; set; } = DateTime.UtcNow;
}
