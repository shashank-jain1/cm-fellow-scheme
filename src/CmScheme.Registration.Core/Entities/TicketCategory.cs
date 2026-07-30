using System.ComponentModel.DataAnnotations;

namespace CmScheme.Registration.Core.Entities;

public class TicketCategory
{
    [Key]
    public int TicketCategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    public string CategoryName { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(20)]
    public string DefaultPriority { get; set; } = "Medium";

    public bool IsActive { get; set; } = true;

    public int SortOrder { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
