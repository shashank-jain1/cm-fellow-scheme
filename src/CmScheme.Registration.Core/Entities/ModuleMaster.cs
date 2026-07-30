using System.ComponentModel.DataAnnotations;

namespace CmScheme.Registration.Core.Entities;

public class ModuleMaster
{
    [Key]
    public int ModuleMasterId { get; set; }

    [Required]
    [MaxLength(100)]
    public string ModuleCode { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    public string ModuleName { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    public int SortOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
