using System.Text.Json;
using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.GrantModuleAccess;

using UserModuleAccessEntity = Core.Entities.UserModuleAccess;

public sealed class GrantModuleAccessCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<GrantModuleAccessCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        GrantModuleAccessCommand request,
        CancellationToken cancellationToken)
    {
        Core.Entities.UserAccount? user = await dbContext.UserAccounts
            .FirstOrDefaultAsync(ua => ua.UserAccountId == request.UserAccountId, cancellationToken);

        if (user is null)
        {
            return Result<int>.NotFound("User account not found.");
        }

        Core.Entities.ModuleMaster? module = await dbContext.ModuleMasters
            .FirstOrDefaultAsync(mm => mm.ModuleMasterId == request.ModuleMasterId, cancellationToken);

        if (module is null)
        {
            return Result<int>.NotFound("Module not found.");
        }

        UserModuleAccessEntity? existingAccess = await dbContext.UserModuleAccesses
            .FirstOrDefaultAsync(uma =>
                uma.UserAccountId == request.UserAccountId &&
                uma.ModuleMasterId == request.ModuleMasterId &&
                uma.DivisionId == request.DivisionId &&
                uma.DistrictId == request.DistrictId &&
                uma.BlockId == request.BlockId,
                cancellationToken);

        string newValues = JsonSerializer.Serialize(new
        {
            request.ModuleMasterId,
            request.CanRead,
            request.CanWrite,
            request.CanApprove,
            request.CanExport,
            request.DivisionId,
            request.DistrictId,
            request.BlockId
        });

        int accessId;

        if (existingAccess is not null)
        {
            string oldValues = JsonSerializer.Serialize(new
            {
                existingAccess.ModuleMasterId,
                existingAccess.CanRead,
                existingAccess.CanWrite,
                existingAccess.CanApprove,
                existingAccess.CanExport,
                existingAccess.DivisionId,
                existingAccess.DistrictId,
                existingAccess.BlockId
            });

            existingAccess.CanRead = request.CanRead;
            existingAccess.CanWrite = request.CanWrite;
            existingAccess.CanApprove = request.CanApprove;
            existingAccess.CanExport = request.CanExport;
            existingAccess.DivisionId = request.DivisionId;
            existingAccess.DistrictId = request.DistrictId;
            existingAccess.BlockId = request.BlockId;
            existingAccess.IsActive = true;

            accessId = existingAccess.UserModuleAccessId;

            dbContext.ModuleAccessAuditLogs.Add(new ModuleAccessAuditLog
            {
                UserModuleAccessId = existingAccess.UserModuleAccessId,
                UserAccountId = request.UserAccountId,
                ModuleMasterId = request.ModuleMasterId,
                Action = "UPDATE",
                OldValues = oldValues,
                NewValues = newValues,
                PerformedBy = request.PerformedBy,
                PerformedOn = DateTime.UtcNow
            });
        }
        else
        {
            UserModuleAccessEntity newAccess = new UserModuleAccessEntity
            {
                UserAccountId = request.UserAccountId,
                ModuleMasterId = request.ModuleMasterId,
                CanRead = request.CanRead,
                CanWrite = request.CanWrite,
                CanApprove = request.CanApprove,
                CanExport = request.CanExport,
                DivisionId = request.DivisionId,
                DistrictId = request.DistrictId,
                BlockId = request.BlockId,
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = request.PerformedBy
            };

            dbContext.UserModuleAccesses.Add(newAccess);
            await dbContext.SaveChangesAsync(cancellationToken);

            accessId = newAccess.UserModuleAccessId;

            dbContext.ModuleAccessAuditLogs.Add(new ModuleAccessAuditLog
            {
                UserModuleAccessId = newAccess.UserModuleAccessId,
                UserAccountId = request.UserAccountId,
                ModuleMasterId = request.ModuleMasterId,
                Action = "GRANT",
                OldValues = null,
                NewValues = newValues,
                PerformedBy = request.PerformedBy,
                PerformedOn = DateTime.UtcNow
            });
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(accessId);
    }
}
