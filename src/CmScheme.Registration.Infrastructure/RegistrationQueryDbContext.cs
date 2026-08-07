using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core.Entities;
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

    IQueryable<ExitInterview> IRegistrationQueryDbContext.ExitInterviews => ExitInterviews;

    IQueryable<Notification> IRegistrationQueryDbContext.Notifications => Notifications;
}
