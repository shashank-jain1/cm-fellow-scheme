using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Application.Features.UserAccount.ListUserAccounts;

public sealed class ListUserAccountsQueryHandler(IRegistrationQueryDbContext dbContext)
    : IQueryHandler<ListUserAccountsQuery, Result<IReadOnlyList<UserAccountListItem>>>
{
    public async ValueTask<Result<IReadOnlyList<UserAccountListItem>>> Handle(
        ListUserAccountsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Core.Entities.UserAccount> query = dbContext.UserAccounts
            .Include(ua => ua.Applicant)
            .AsNoTracking();

        if (!string.IsNullOrEmpty(request.Role))
        {
            query = query.Where(ua => ua.Role == request.Role);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(ua => ua.IsActive == request.IsActive.Value);
        }

        IReadOnlyList<UserAccountListItem> result = await query
            .OrderByDescending(ua => ua.CreatedOn)
            .Select(ua => new UserAccountListItem
            {
                UserAccountId = ua.UserAccountId,
                ApplicantId = ua.ApplicantId,
                Username = ua.Username,
                Role = ua.Role,
                IsActive = ua.IsActive,
                FirstName = ua.Applicant.FirstName,
                LastName = ua.Applicant.LastName,
                EmailId = ua.Applicant.EmailId,
                MobileNumber = ua.Applicant.MobileNumber,
                CreatedOn = ua.CreatedOn,
            })
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<UserAccountListItem>>.Success(result);
    }
}
