using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmScheme.Registration.Core.Entities;

public class ModuleAccessAuditLog
{
    [Key]
    public int ModuleAccessAuditLogId { get; set; }

    public int UserModuleAccessId { get; set; }

    public int UserAccountId { get; set; }

    public int ModuleMasterId { get; set; }

    [Required]
    [MaxLength(20)]
    public string Action { get; set; } = null!;

    [MaxLength(500)]
    public string? OldValues { get; set; }

    [MaxLength(500)]
    public string? NewValues { get; set; }

    public int PerformedBy { get; set; }

    public DateTime PerformedOn { get; set; } = DateTime.UtcNow;

    [MaxLength(500)]
    public string? Reason { get; set; }

    [ForeignKey(nameof(UserAccountId))]
    public UserAccount? UserAccount { get; set; }

    [ForeignKey(nameof(ModuleMasterId))]
    public ModuleMaster? ModuleMaster { get; set; }

    [ForeignKey(nameof(PerformedBy))]
    public UserAccount? Performer { get; set; }
}
