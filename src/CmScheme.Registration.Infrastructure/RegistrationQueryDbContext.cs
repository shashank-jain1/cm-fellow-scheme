using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Infrastructure;

public sealed class RegistrationQueryDbContext : RegistrationDbContext, IRegistrationQueryDbContext
{
    public RegistrationQueryDbContext(DbContextOptions<RegistrationQueryDbContext> options)
        : base(options)
    {
    }

    IQueryable<Applicant> IRegistrationQueryDbContext.Applicants => Applicants;

    IQueryable<UserAccount> IRegistrationQueryDbContext.UserAccounts => UserAccounts;
}
