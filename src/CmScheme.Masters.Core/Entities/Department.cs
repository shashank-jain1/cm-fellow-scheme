using System.ComponentModel.DataAnnotations;

namespace CmScheme.Masters.Core.Entities;

public class Department
{
    [Key]
    public int DepartmentId { get; set; }

    [Required]
    [MaxLength(150)]
    public string DepartmentName { get; set; } = null!;

    [MaxLength(50)]
    public string? DepartmentCode { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;

    public int? ModifiedBy { get; set; }
}
