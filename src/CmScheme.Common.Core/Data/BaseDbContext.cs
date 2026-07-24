using Microsoft.EntityFrameworkCore;

namespace CmScheme.Common.Core.Data;

public abstract class BaseDbContext(DbContextOptions options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
