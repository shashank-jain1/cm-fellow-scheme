namespace CmScheme.Performance.Core.Views;

public class AttendanceView
{
    public int AttendanceId { get; set; }
    public int UserAccountId { get; set; }
    public DateTime AttendanceDate { get; set; }
    public string Status { get; set; } = null!;
}
