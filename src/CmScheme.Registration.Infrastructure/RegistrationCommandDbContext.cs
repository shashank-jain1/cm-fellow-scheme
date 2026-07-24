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

    async Task<int> IRegistrationCommandDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
