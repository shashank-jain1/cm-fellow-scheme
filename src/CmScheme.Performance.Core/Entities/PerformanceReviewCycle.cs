using System.ComponentModel.DataAnnotations;

namespace CmScheme.Performance.Core.Entities;

public class PerformanceReviewCycle
{
    [Key]
    public int ReviewCycleId { get; set; }
    [Required]
    [MaxLength(100)]
    public string CycleName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
