using System.ComponentModel.DataAnnotations;

namespace CmScheme.Registration.Core.Entities;

public class AuditLog
{
    [Key]
    public long AuditLogId { get; set; }

    public int? UserId { get; set; }

    [MaxLength(50)]
    public string? Action { get; set; }

    [MaxLength(100)]
    public string? EntityName { get; set; }

    [MaxLength(50)]
    public string? EntityId { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    [MaxLength(45)]
    public string? IpAddress { get; set; }

    [MaxLength(500)]
    public string? UserAgent { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
