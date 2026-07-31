namespace CmScheme.AttendanceLeave.Core.Dtos;

public sealed class LeaveBalanceDto
{
    public int LeaveBalanceId { get; init; }
    public int ApplicantId { get; init; }
    public string LeaveType { get; init; } = null!;
    public int OpeningBalance { get; init; }
    public int AvailedLeave { get; init; }
    public int PendingApprovalLeave { get; init; }
    public int AvailableBalance { get; init; }
}
