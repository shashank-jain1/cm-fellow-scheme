using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core.Data;
using CmScheme.Common.Core.Entities;
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

    public DbSet<LeaveType> LeaveTypes => Set<LeaveType>();

    public DbSet<LeaveBalance> LeaveBalances => Set<LeaveBalance>();

    public DbSet<LeaveApplication> LeaveApplications => Set<LeaveApplication>();

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    public DbSet<TrainingEnrollment> TrainingEnrollments => Set<TrainingEnrollment>();

    public DbSet<TicketCategory> TicketCategories => Set<TicketCategory>();

    public DbSet<ExitInterview> ExitInterviews => Set<ExitInterview>();

    public DbSet<TrainingAttendance> TrainingAttendances => Set<TrainingAttendance>();

    public DbSet<Notification> Notifications => Set<Notification>();

    public DbSet<SystemConfig> SystemConfigs => Set<SystemConfig>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ApplicantConfiguration());
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new ModuleMasterConfiguration());
        modelBuilder.ApplyConfiguration(new UserModuleAccessConfiguration());
        modelBuilder.ApplyConfiguration(new ModuleAccessAuditLogConfiguration());
        modelBuilder.ApplyConfiguration(new LeaveTypeConfiguration());
        modelBuilder.ApplyConfiguration(new LeaveBalanceConfiguration());
        modelBuilder.ApplyConfiguration(new LeaveApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
        modelBuilder.ApplyConfiguration(new TrainingEnrollmentConfiguration());
        modelBuilder.ApplyConfiguration(new TicketCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ExitInterviewConfiguration());

        modelBuilder.Entity<Notification>().ToTable("Notification");
        modelBuilder.Entity<SystemConfig>().ToTable("SystemConfigs");
        modelBuilder.Entity<TrainingAttendance>().ToTable("TrainingAttendances");
    }
}
