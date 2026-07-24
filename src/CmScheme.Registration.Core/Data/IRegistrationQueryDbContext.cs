using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data;

public interface IRegistrationQueryDbContext
{
    IQueryable<Applicant> Applicants { get; }

    IQueryable<UserAccount> UserAccounts { get; }
}
