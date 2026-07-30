namespace CmScheme.Dashboard.Core.Entities;

public class DashboardWidget
{
    public int WidgetId { get; set; }
    public string WidgetName { get; set; } = null!;
    public string WidgetType { get; set; } = null!;
    public string DataSourceQuery { get; set; } = null!;
    public string RoleAccess { get; set; } = null!;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}
