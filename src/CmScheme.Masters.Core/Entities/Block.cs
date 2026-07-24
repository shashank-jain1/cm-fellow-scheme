using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmScheme.Masters.Core.Entities;

public class Block
{
    [Key]
    public int BlockId { get; set; }
    public int DistrictId { get; set; }
    [ForeignKey(nameof(DistrictId))]
    public District District { get; set; } = null!;
    [Required]
    [MaxLength(100)]
    public string BlockName { get; set; } = null!;
    [MaxLength(10)]
    public string? BlockCode { get; set; }
    public bool IsActive { get; set; } = true;
}
