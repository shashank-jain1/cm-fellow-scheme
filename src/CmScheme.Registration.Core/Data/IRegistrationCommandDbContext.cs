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

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
