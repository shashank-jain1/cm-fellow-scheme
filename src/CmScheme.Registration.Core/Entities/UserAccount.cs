using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmScheme.Registration.Core.Entities;

public class UserAccount
{
    [Key]
    public int UserAccountId { get; set; }

    public int ApplicantId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public int? CreatedBy { get; set; }

    [ForeignKey(nameof(ApplicantId))]
    public Applicant? Applicant { get; set; }
}
