using System.ComponentModel.DataAnnotations;

namespace CmScheme.Masters.Core.Entities;

public class State
{
    [Key]
    public int StateId { get; set; }
    [Required]
    [MaxLength(100)]
    public string StateName { get; set; } = null!;
    [Required]
    [MaxLength(10)]
    public string StateCode { get; set; } = null!;
    [MaxLength(20)]
    public string? StateShortName { get; set; }
    public int? DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public int? CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    public int? ModifiedBy { get; set; }
}
