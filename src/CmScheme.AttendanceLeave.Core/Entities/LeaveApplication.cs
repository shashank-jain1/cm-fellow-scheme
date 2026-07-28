namespace CmScheme.AttendanceLeave.Core.Entities;

public class LeaveApplication
{
    public int LeaveApplicationId { get; set; }
    public int ApplicantId { get; set; }
    public string LeaveType { get; set; } = null!;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public decimal NumberOfDays { get; set; }
    public string HalfDayFullDay { get; set; } = null!;
    public string LeaveReason { get; set; } = null!;
    public string? AttachmentPath { get; set; }
    public string ReportingManagerName { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedOn { get; set; }
}
