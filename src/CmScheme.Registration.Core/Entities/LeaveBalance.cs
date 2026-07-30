using System.ComponentModel.DataAnnotations;

namespace CmScheme.Registration.Core.Entities;

public class LeaveBalance
{
    [Key]
    public int LeaveBalanceId { get; set; }

    public int UserAccountId { get; set; }

    [Required]
    public UserAccount UserAccount { get; set; } = null!;

    public int LeaveTypeId { get; set; }

    [Required]
    public LeaveType LeaveType { get; set; } = null!;

    public int Year { get; set; }

    public decimal TotalDays { get; set; }

    public decimal UsedDays { get; set; } = 0;

    public decimal RemainingDays { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
