using System.ComponentModel.DataAnnotations;

namespace CmScheme.Masters.Core.Entities;

public class LookupMaster
{
    [Key]
    public int LookupMasterId { get; set; }

    [Required]
    [MaxLength(50)]
    public string MasterType { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Label { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Value { get; set; } = null!;

    public int SortOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
