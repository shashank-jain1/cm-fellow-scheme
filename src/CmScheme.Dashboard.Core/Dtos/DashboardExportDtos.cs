namespace CmScheme.Dashboard.Core.Dtos;

public sealed class DashboardWidgetDto
{
    public int WidgetId { get; init; }
    public string WidgetName { get; init; } = null!;
    public string WidgetType { get; init; } = null!;
    public string RoleAccess { get; init; } = null!;
    public int SortOrder { get; init; }
    public string? LatestData { get; init; }
}

public sealed class RoleDashboardDto
{
    public string Role { get; init; } = null!;
    public List<DashboardWidgetDto> Widgets { get; init; } = [];
}

public sealed class FellowDashboardDto
{
    public int TotalTickets { get; init; }
    public int OpenTickets { get; init; }
    public decimal AttendancePercentage { get; init; }
    public int LeaveBalanceDays { get; init; }
    public int PendingTasks { get; init; }
    public int CompletedTasks { get; init; }
    public decimal PerformanceScore { get; init; }

    // Survey-centric progress, as the Monitoring spec defines for a fellow's own view.
    public int TotalAssignedProjects { get; init; }
    public int CompletedSurveys { get; init; }
    public int PendingSurveys { get; init; }
    public int UpcomingTraining { get; init; }
    public string RecentActivity { get; init; } = string.Empty;
}

public sealed class DashboardExportDto
{
    public string Format { get; init; } = null!;
    public byte[] FileContents { get; init; } = [];
    public string FileName { get; init; } = null!;
    public string ContentType { get; init; } = null!;
}
