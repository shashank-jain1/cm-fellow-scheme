using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data;

public interface IRegistrationCommandDbContext
{
    DbSet<Applicant> Applicants { get; }

    DbSet<UserAccount> UserAccounts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
