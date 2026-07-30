using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmScheme.Registration.Core.Entities;

public class UserModuleAccess
{
    [Key]
    public int UserModuleAccessId { get; set; }

    public int UserAccountId { get; set; }

    public int ModuleMasterId { get; set; }

    public bool CanRead { get; set; } = true;

    public bool CanWrite { get; set; } = false;

    public bool CanApprove { get; set; } = false;

    public bool CanExport { get; set; } = false;

    public int? DivisionId { get; set; }

    public int? DistrictId { get; set; }

    public int? BlockId { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public int? CreatedBy { get; set; }

    [ForeignKey(nameof(UserAccountId))]
    public UserAccount? UserAccount { get; set; }

    [ForeignKey(nameof(ModuleMasterId))]
    public ModuleMaster? ModuleMaster { get; set; }
}
