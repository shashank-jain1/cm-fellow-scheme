using System.ComponentModel.DataAnnotations;

namespace CmScheme.Performance.Core.Entities;

public class ImprovementPlan
{
    [Key]
    public int ImprovementPlanId { get; set; }
    public int UserAccountId { get; set; }
    [Required]
    [MaxLength(200)]
    public string PlanTitle { get; set; } = null!;
    [MaxLength(1000)]
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [MaxLength(20)]
    public string Status { get; set; } = "Active";
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
