using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Dtos;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.GetUserModuleAccess;

public sealed class GetUserModuleAccessQueryHandler(IRegistrationCommandDbContext dbContext)
    : IQueryHandler<GetUserModuleAccessQuery, Result<List<ModuleAccessDto>>>
{
    public async ValueTask<Result<List<ModuleAccessDto>>> Handle(
        GetUserModuleAccessQuery request,
        CancellationToken cancellationToken)
    {
        Core.Entities.UserAccount? user = await dbContext.UserAccounts
            .Include(ua => ua.Applicant)
            .FirstOrDefaultAsync(ua => ua.UserAccountId == request.UserAccountId, cancellationToken);

        if (user is null)
        {
            return Result<List<ModuleAccessDto>>.NotFound("User account not found.");
        }

        string fullName = $"{user.Applicant?.FirstName} {user.Applicant?.LastName}".Trim();

        List<ModuleAccessDto> accesses = await dbContext.UserModuleAccesses
            .Include(uma => uma.ModuleMaster)
            .AsNoTracking()
            .Where(uma => uma.UserAccountId == request.UserAccountId)
            .Select(uma => new ModuleAccessDto
            {
                UserModuleAccessId = uma.UserModuleAccessId,
                UserAccountId = uma.UserAccountId,
                Username = user.Username,
                FullName = fullName,
                ModuleMasterId = uma.ModuleMasterId,
                ModuleCode = uma.ModuleMaster!.ModuleCode,
                ModuleName = uma.ModuleMaster.ModuleName,
                CanRead = uma.CanRead,
                CanWrite = uma.CanWrite,
                CanApprove = uma.CanApprove,
                CanExport = uma.CanExport,
                DivisionId = uma.DivisionId,
                DistrictId = uma.DistrictId,
                BlockId = uma.BlockId,
                IsActive = uma.IsActive
            })
            .ToListAsync(cancellationToken);

        return Result<List<ModuleAccessDto>>.Success(accesses);
    }
}
