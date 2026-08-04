using CmScheme.Dashboard.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Dashboard.Core.Data;

public interface IDashboardQueryDbContext
{
    IQueryable<DashboardWidget> DashboardWidgets { get; }
    IQueryable<DashboardSnapshot> DashboardSnapshots { get; }

    IQueryable<CmScheme.Dashboard.Core.Views.UserAccountView> UserAccountViews { get; }
    IQueryable<CmScheme.Dashboard.Core.Views.AttendanceView> AttendanceViews { get; }
    IQueryable<CmScheme.Dashboard.Core.Views.LeaveApplicationView> LeaveApplicationViews { get; }
    IQueryable<CmScheme.Dashboard.Core.Views.TicketView> TicketViews { get; }
}
