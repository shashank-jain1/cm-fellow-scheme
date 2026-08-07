using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core.Entities;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data;

public interface IRegistrationQueryDbContext
{
    IQueryable<Applicant> Applicants { get; }

    IQueryable<UserAccount> UserAccounts { get; }

    IQueryable<ExitInterview> ExitInterviews { get; }

    IQueryable<Notification> Notifications { get; }
}
