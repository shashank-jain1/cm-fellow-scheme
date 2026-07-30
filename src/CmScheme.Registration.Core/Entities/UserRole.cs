using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmScheme.Registration.Core.Entities;

public class UserRole
{
    [Key]
    public int UserRoleId { get; set; }

    public int UserAccountId { get; set; }

    public int RoleLookupId { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public int? CreatedBy { get; set; }

    [ForeignKey(nameof(UserAccountId))]
    public UserAccount? UserAccount { get; set; }
}
