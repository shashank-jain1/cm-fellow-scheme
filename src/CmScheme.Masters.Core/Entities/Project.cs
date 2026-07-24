using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmScheme.Masters.Core.Entities;

public class Project
{
    [Key]
    public int ProjectId { get; set; }
    [Required]
    [MaxLength(250)]
    public string ProjectName { get; set; } = null!;
    [Required]
    [MaxLength(20)]
    public string ProjectCode { get; set; } = null!;
    [MaxLength(1000)]
    public string? ProjectDescription { get; set; }
    [Required]
    [MaxLength(150)]
    public string DepartmentName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [Required]
    [MaxLength(150)]
    public string ProjectIncharge { get; set; } = null!;
    [Column(TypeName = "decimal(15,2)")]
    public decimal? BudgetAmount { get; set; }
    public int BudgetApprovedBy { get; set; }
    [MaxLength(255)]
    public string? ProjectDocumentPath { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public int? CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    public int? ModifiedBy { get; set; }
}
