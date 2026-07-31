namespace CmScheme.AttendanceLeave.Core.Dtos;

public sealed class LeaveApplicationDto
{
    public int LeaveApplicationId { get; init; }
    public int ApplicantId { get; init; }
    public string LeaveType { get; init; } = null!;
    public DateTime FromDate { get; init; }
    public DateTime ToDate { get; init; }
    public decimal NumberOfDays { get; init; }
    public string HalfDayFullDay { get; init; } = null!;
    public string LeaveReason { get; init; } = null!;
    public string? AttachmentPath { get; init; }
    public string ReportingManagerName { get; init; } = null!;
    public string Status { get; init; } = null!;
    public DateTime CreatedOn { get; init; }
    public string CreatedBy { get; init; } = null!;
    public DateTime? ModifiedOn { get; init; }
}
