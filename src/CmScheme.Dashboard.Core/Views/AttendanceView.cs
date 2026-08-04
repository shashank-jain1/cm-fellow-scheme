namespace CmScheme.Dashboard.Core.Views;

public class AttendanceView
{
    public int AttendanceId { get; set; }
    public DateTime AttendanceDate { get; set; }
    public string Status { get; set; } = null!;
}
