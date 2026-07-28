using System.ComponentModel.DataAnnotations;

namespace CmScheme.AttendanceLeave.Core.Entities;

public class Holiday
{
    public int HolidayId { get; set; }

    [Required]
    [MaxLength(200)]
    public string HolidayName { get; set; } = null!;

    public DateTime HolidayDate { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool IsOptional { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
