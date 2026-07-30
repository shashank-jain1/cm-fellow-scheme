using CmScheme.Dashboard.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Dashboard.Core.Data;

public interface IDashboardCommandDbContext
{
    DbSet<DashboardWidget> DashboardWidgets { get; }
    DbSet<DashboardSnapshot> DashboardSnapshots { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
