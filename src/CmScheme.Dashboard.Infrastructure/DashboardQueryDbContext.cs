using CmScheme.Dashboard.Core.Data;
using CmScheme.Dashboard.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Dashboard.Infrastructure;

public class DashboardQueryDbContext : IDashboardQueryDbContext
{
    private readonly DashboardDbContext _dbContext;

    public DashboardQueryDbContext(DashboardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<DashboardWidget> DashboardWidgets => _dbContext.DashboardWidgets;
    public IQueryable<DashboardSnapshot> DashboardSnapshots => _dbContext.DashboardSnapshots;

    public IQueryable<CmScheme.Dashboard.Core.Views.UserAccountView> UserAccountViews => _dbContext.UserAccountViews;
    public IQueryable<CmScheme.Dashboard.Core.Views.AttendanceView> AttendanceViews => _dbContext.AttendanceViews;
    public IQueryable<CmScheme.Dashboard.Core.Views.LeaveApplicationView> LeaveApplicationViews => _dbContext.LeaveApplicationViews;
    public IQueryable<CmScheme.Dashboard.Core.Views.TicketView> TicketViews => _dbContext.TicketViews;
}
