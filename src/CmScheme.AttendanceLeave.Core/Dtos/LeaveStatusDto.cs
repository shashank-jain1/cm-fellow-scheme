namespace CmScheme.AttendanceLeave.Core.Dtos;

public sealed class LeaveStatusDto
{
    public int LeaveApplicationId { get; init; }
    public string LeaveType { get; init; } = null!;
    public DateTime FromDate { get; init; }
    public DateTime ToDate { get; init; }
    public decimal NumberOfDays { get; init; }
    public string Status { get; init; } = null!;
    public DateTime CreatedOn { get; init; }
}
