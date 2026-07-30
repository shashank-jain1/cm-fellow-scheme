using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core.Data;
using CmScheme.Registration.Core.Entities;
using CmScheme.Registration.Core.Data.Configurations;

namespace CmScheme.Registration.Infrastructure;

public abstract class RegistrationDbContext : BaseDbContext
{
    protected RegistrationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Applicant> Applicants => Set<Applicant>();

    public DbSet<UserAccount> UserAccounts => Set<UserAccount>();

    public DbSet<UserRole> UserRoles => Set<UserRole>();

    public DbSet<ModuleMaster> ModuleMasters => Set<ModuleMaster>();

    public DbSet<UserModuleAccess> UserModuleAccesses => Set<UserModuleAccess>();

    public DbSet<ModuleAccessAuditLog> ModuleAccessAuditLogs => Set<ModuleAccessAuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ApplicantConfiguration());
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new ModuleMasterConfiguration());
        modelBuilder.ApplyConfiguration(new UserModuleAccessConfiguration());
        modelBuilder.ApplyConfiguration(new ModuleAccessAuditLogConfiguration());
    }
}
