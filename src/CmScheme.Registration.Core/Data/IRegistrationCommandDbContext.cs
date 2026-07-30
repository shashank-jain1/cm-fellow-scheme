using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data;

public interface IRegistrationCommandDbContext
{
    DbSet<Applicant> Applicants { get; }

    DbSet<UserAccount> UserAccounts { get; }

    DbSet<UserRole> UserRoles { get; }

    DbSet<ModuleMaster> ModuleMasters { get; }

    DbSet<UserModuleAccess> UserModuleAccesses { get; }

    DbSet<ModuleAccessAuditLog> ModuleAccessAuditLogs { get; }

    DbSet<LeaveType> LeaveTypes { get; }

    DbSet<LeaveBalance> LeaveBalances { get; }

    DbSet<LeaveApplication> LeaveApplications { get; }

    DbSet<AuditLog> AuditLogs { get; }

    DbSet<TrainingEnrollment> TrainingEnrollments { get; }

    DbSet<TicketCategory> TicketCategories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
