using CmScheme.Dashboard.Core.Data;
using CmScheme.Dashboard.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Dashboard.Infrastructure;

public class DashboardCommandDbContext : IDashboardCommandDbContext
{
    private readonly DashboardDbContext _dbContext;

    public DashboardCommandDbContext(DashboardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbSet<DashboardWidget> DashboardWidgets => _dbContext.DashboardWidgets;
    public DbSet<DashboardSnapshot> DashboardSnapshots => _dbContext.DashboardSnapshots;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
