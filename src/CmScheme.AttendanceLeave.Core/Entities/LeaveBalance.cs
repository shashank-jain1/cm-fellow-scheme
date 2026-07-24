namespace CmScheme.AttendanceLeave.Core.Entities;

public class LeaveBalance
{
    public int LeaveBalanceId { get; set; }
    public int ApplicantId { get; set; }
    public string LeaveType { get; set; } = null!;
    public int OpeningBalance { get; set; }
    public int AvailedLeave { get; set; }
    public int PendingApprovalLeave { get; set; }
    public int AvailableBalance { get; set; }
}
