using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmScheme.Masters.Core.Entities;

public class District
{
    [Key]
    public int DistrictId { get; set; }
    public int DivisionId { get; set; }
    [ForeignKey(nameof(DivisionId))]
    public Division Division { get; set; } = null!;
    [Required]
    [MaxLength(100)]
    public string DistrictName { get; set; } = null!;
    [MaxLength(10)]
    public string? DistrictCode { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public int? CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    public int? ModifiedBy { get; set; }
}
