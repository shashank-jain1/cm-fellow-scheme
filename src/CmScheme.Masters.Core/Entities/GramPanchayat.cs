using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmScheme.Masters.Core.Entities;

public class GramPanchayat
{
    [Key]
    public int GramPanchayatId { get; set; }
    public int BlockId { get; set; }
    [ForeignKey(nameof(BlockId))]
    public Block Block { get; set; } = null!;
    [Required]
    [MaxLength(150)]
    public string GramPanchayatName { get; set; } = null!;
    [MaxLength(20)]
    public string? GPCode { get; set; }
    public bool IsActive { get; set; } = true;
}
