using CmScheme.Common.Core.Data;
using CmScheme.Dashboard.Core.Data;
using CmScheme.Dashboard.Core.Data.Configurations;
using CmScheme.Dashboard.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Dashboard.Infrastructure;

public class DashboardDbContext : BaseDbContext, IDashboardCommandDbContext, IDashboardQueryDbContext
{
    public DbSet<DashboardWidget> DashboardWidgets => Set<DashboardWidget>();
    public DbSet<DashboardSnapshot> DashboardSnapshots => Set<DashboardSnapshot>();

    public DbSet<CmScheme.Dashboard.Core.Views.UserAccountView> UserAccountViews => Set<CmScheme.Dashboard.Core.Views.UserAccountView>();
    public DbSet<CmScheme.Dashboard.Core.Views.AttendanceView> AttendanceViews => Set<CmScheme.Dashboard.Core.Views.AttendanceView>();
    public DbSet<CmScheme.Dashboard.Core.Views.LeaveApplicationView> LeaveApplicationViews => Set<CmScheme.Dashboard.Core.Views.LeaveApplicationView>();
    public DbSet<CmScheme.Dashboard.Core.Views.TicketView> TicketViews => Set<CmScheme.Dashboard.Core.Views.TicketView>();

    public DashboardDbContext(DbContextOptions<DashboardDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new DashboardWidgetConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardSnapshotConfiguration());

        modelBuilder.Entity<CmScheme.Dashboard.Core.Views.UserAccountView>(b =>
        {
            b.HasNoKey();
            b.ToTable("UserAccount", t => t.ExcludeFromMigrations());
        });

        modelBuilder.Entity<CmScheme.Dashboard.Core.Views.AttendanceView>(b =>
        {
            b.HasNoKey();
            b.ToTable("Attendance", t => t.ExcludeFromMigrations());
        });

        modelBuilder.Entity<CmScheme.Dashboard.Core.Views.LeaveApplicationView>(b =>
        {
            b.HasNoKey();
            b.ToTable("LeaveApplication", t => t.ExcludeFromMigrations());
        });

        modelBuilder.Entity<CmScheme.Dashboard.Core.Views.TicketView>(b =>
        {
            b.HasNoKey();
            b.ToTable("Ticket", t => t.ExcludeFromMigrations());
        });
    }

    IQueryable<DashboardWidget> IDashboardQueryDbContext.DashboardWidgets => DashboardWidgets;
    IQueryable<DashboardSnapshot> IDashboardQueryDbContext.DashboardSnapshots => DashboardSnapshots;

    IQueryable<CmScheme.Dashboard.Core.Views.UserAccountView> IDashboardQueryDbContext.UserAccountViews => UserAccountViews.AsNoTracking();
    IQueryable<CmScheme.Dashboard.Core.Views.AttendanceView> IDashboardQueryDbContext.AttendanceViews => AttendanceViews.AsNoTracking();
    IQueryable<CmScheme.Dashboard.Core.Views.LeaveApplicationView> IDashboardQueryDbContext.LeaveApplicationViews => LeaveApplicationViews.AsNoTracking();
    IQueryable<CmScheme.Dashboard.Core.Views.TicketView> IDashboardQueryDbContext.TicketViews => TicketViews.AsNoTracking();
}
