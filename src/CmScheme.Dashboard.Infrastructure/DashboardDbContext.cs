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

    public DashboardDbContext(DbContextOptions<DashboardDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new DashboardWidgetConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardSnapshotConfiguration());
    }

    IQueryable<DashboardWidget> IDashboardQueryDbContext.DashboardWidgets => DashboardWidgets;
    IQueryable<DashboardSnapshot> IDashboardQueryDbContext.DashboardSnapshots => DashboardSnapshots;
}
