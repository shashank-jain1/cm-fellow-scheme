using System.ComponentModel.DataAnnotations;

namespace CmScheme.Registration.Core.Entities;

public class LeaveType
{
    [Key]
    public int LeaveTypeId { get; set; }

    [Required]
    [MaxLength(50)]
    public string TypeName { get; set; } = null!;

    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = null!;

    public int DefaultDays { get; set; }

    public bool IsActive { get; set; } = true;

    public int SortOrder { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
