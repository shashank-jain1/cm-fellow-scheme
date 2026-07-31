using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Dtos;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.GetAllModuleAccess;

public sealed class GetAllModuleAccessQueryHandler(IRegistrationCommandDbContext dbContext)
    : IQueryHandler<GetAllModuleAccessQuery, Result<List<UserAccessSummaryDto>>>
{
    public async ValueTask<Result<List<UserAccessSummaryDto>>> Handle(
        GetAllModuleAccessQuery request,
        CancellationToken cancellationToken)
    {
        List<Core.Entities.UserAccount> users = await dbContext.UserAccounts
            .Include(ua => ua.Applicant)
            .AsNoTracking()
            .OrderBy(ua => ua.Username)
            .ToListAsync(cancellationToken);

        List<int> userIds = users.Select(u => u.UserAccountId).ToList();

        List<Core.Entities.UserModuleAccess> allAccess = await dbContext.UserModuleAccesses
            .Include(uma => uma.ModuleMaster)
            .Where(uma => userIds.Contains(uma.UserAccountId))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        List<UserAccessSummaryDto> result = users
            .Select(user =>
            {
                List<Core.Entities.UserModuleAccess> userAccess = allAccess
                    .Where(uma => uma.UserAccountId == user.UserAccountId)
                    .ToList();

                return new UserAccessSummaryDto
                {
                    UserAccountId = user.UserAccountId,
                    Username = user.Username,
                    FullName = $"{user.Applicant?.FirstName} {user.Applicant?.LastName}".Trim(),
                    Role = user.Role,
                    DivisionId = user.Applicant?.DivisionId,
                    ModuleAccesses = userAccess
                        .Where(uma => uma.IsActive)
                        .Select(uma => new ModuleAccessDto
                        {
                            UserModuleAccessId = uma.UserModuleAccessId,
                            UserAccountId = uma.UserAccountId,
                            Username = user.Username,
                            FullName = $"{user.Applicant?.FirstName} {user.Applicant?.LastName}".Trim(),
                            ModuleMasterId = uma.ModuleMasterId,
                            ModuleCode = uma.ModuleMaster?.ModuleCode ?? string.Empty,
                            ModuleName = uma.ModuleMaster?.ModuleName ?? string.Empty,
                            CanRead = uma.CanRead,
                            CanWrite = uma.CanWrite,
                            CanApprove = uma.CanApprove,
                            CanExport = uma.CanExport,
                            DivisionId = uma.DivisionId,
                            DistrictId = uma.DistrictId,
                            BlockId = uma.BlockId,
                            IsActive = uma.IsActive
                        })
                        .ToList()
                };
            })
            .ToList();

        return Result<List<UserAccessSummaryDto>>.Success(result);
    }
}
