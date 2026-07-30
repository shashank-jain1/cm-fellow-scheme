using CmScheme.Dashboard.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Dashboard.Core.Data;

public interface IDashboardQueryDbContext
{
    IQueryable<DashboardWidget> DashboardWidgets { get; }
    IQueryable<DashboardSnapshot> DashboardSnapshots { get; }
}
