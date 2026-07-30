using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Infrastructure;

public sealed class RegistrationCommandDbContext : RegistrationDbContext, IRegistrationCommandDbContext
{
    public RegistrationCommandDbContext(DbContextOptions<RegistrationCommandDbContext> options)
        : base(options)
    {
    }

    DbSet<Applicant> IRegistrationCommandDbContext.Applicants => Applicants;

    DbSet<UserAccount> IRegistrationCommandDbContext.UserAccounts => UserAccounts;

    DbSet<UserRole> IRegistrationCommandDbContext.UserRoles => UserRoles;

    DbSet<ModuleMaster> IRegistrationCommandDbContext.ModuleMasters => ModuleMasters;

    DbSet<UserModuleAccess> IRegistrationCommandDbContext.UserModuleAccesses => UserModuleAccesses;

    DbSet<ModuleAccessAuditLog> IRegistrationCommandDbContext.ModuleAccessAuditLogs => ModuleAccessAuditLogs;

    DbSet<LeaveType> IRegistrationCommandDbContext.LeaveTypes => LeaveTypes;

    DbSet<LeaveBalance> IRegistrationCommandDbContext.LeaveBalances => LeaveBalances;

    DbSet<LeaveApplication> IRegistrationCommandDbContext.LeaveApplications => LeaveApplications;

    DbSet<AuditLog> IRegistrationCommandDbContext.AuditLogs => AuditLogs;

    DbSet<TrainingEnrollment> IRegistrationCommandDbContext.TrainingEnrollments => TrainingEnrollments;

    DbSet<TicketCategory> IRegistrationCommandDbContext.TicketCategories => TicketCategories;

    DbSet<ExitInterview> IRegistrationCommandDbContext.ExitInterviews => ExitInterviews;

    async Task<int> IRegistrationCommandDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
