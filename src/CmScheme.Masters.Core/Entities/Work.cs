using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmScheme.Masters.Core.Entities;

public class Work
{
    [Key]
    public int WorkId { get; set; }
    public int ProjectId { get; set; }
    [ForeignKey(nameof(ProjectId))]
    public Project Project { get; set; } = null!;
    [Required]
    [MaxLength(250)]
    public string WorkName { get; set; } = null!;
    [MaxLength(1000)]
    public string? WorkDescription { get; set; }
    [Required]
    [MaxLength(20)]
    public string Priority { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [Required]
    [MaxLength(150)]
    public string AssignedTo { get; set; } = null!;
    [MaxLength(500)]
    public string? Remarks { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public int? CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    public int? ModifiedBy { get; set; }
}
