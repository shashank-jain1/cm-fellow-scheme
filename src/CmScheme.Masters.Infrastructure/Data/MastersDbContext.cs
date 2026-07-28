using Microsoft.EntityFrameworkCore;

using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Infrastructure.Data;

public abstract class MastersDbContext : DbContext
{
    protected MastersDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<State> States => Set<State>();
    public DbSet<Division> Divisions => Set<Division>();
    public DbSet<District> Districts => Set<District>();
    public DbSet<Block> Blocks => Set<Block>();
    public DbSet<GramPanchayat> GramPanchayats => Set<GramPanchayat>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Work> Works => Set<Work>();
    public DbSet<LookupMaster> LookupMasters => Set<LookupMaster>();
    public DbSet<TrainingSchedule> TrainingSchedules => Set<TrainingSchedule>();
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MastersDbContext).Assembly);
    }
}
