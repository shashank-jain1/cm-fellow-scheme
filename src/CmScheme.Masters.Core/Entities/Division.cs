using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmScheme.Masters.Core.Entities;

public class Division
{
    [Key]
    public int DivisionId { get; set; }
    public int StateId { get; set; }
    [ForeignKey(nameof(StateId))]
    public State State { get; set; } = null!;
    [Required]
    [MaxLength(100)]
    public string DivisionName { get; set; } = null!;
    [MaxLength(10)]
    public string? DivisionCode { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public int? CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    public int? ModifiedBy { get; set; }
}
