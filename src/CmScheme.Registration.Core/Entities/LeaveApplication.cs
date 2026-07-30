using System.ComponentModel.DataAnnotations;

namespace CmScheme.Registration.Core.Entities;

public class LeaveApplication
{
    [Key]
    public int LeaveApplicationId { get; set; }

    [Required]
    [MaxLength(20)]
    public string ApplicationNumber { get; set; } = null!;

    public int UserAccountId { get; set; }

    [Required]
    public UserAccount UserAccount { get; set; } = null!;

    public int LeaveTypeId { get; set; }

    [Required]
    public LeaveType LeaveType { get; set; } = null!;

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public decimal NumberOfDays { get; set; }

    public bool IsHalfDay { get; set; } = false;

    [Required]
    [MaxLength(250)]
    public string Reason { get; set; } = null!;

    [MaxLength(255)]
    public string? AttachmentPath { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = null!;

    public int? ApprovedBy { get; set; }

    public UserAccount? ApprovedByUser { get; set; }

    [MaxLength(500)]
    public string? ApprovalRemarks { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public int? CreatedBy { get; set; }
}
