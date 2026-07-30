namespace CmScheme.Dashboard.Core.Entities;

public class DashboardSnapshot
{
    public long SnapshotId { get; set; }
    public int WidgetId { get; set; }
    public DateTime SnapshotDate { get; set; }
    public string JsonData { get; set; } = null!;
}
