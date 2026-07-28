using System.ComponentModel.DataAnnotations;

namespace CmScheme.AttendanceLeave.Core.Entities;

public class PayrollAttendanceSummary
{
    [Key]
    public int PayrollAttendanceSummaryId { get; set; }

    public int ApplicantId { get; set; }

    [Required]
    [MaxLength(20)]
    public string PayrollMonth { get; set; } = null!;

    public int TotalWorkingDays { get; set; }

    public decimal PresentDays { get; set; }

    public decimal ApprovedLeaveDays { get; set; }

    public decimal AbsentDays { get; set; }

    public decimal PayableDays { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;

    public int? ModifiedBy { get; set; }
}
