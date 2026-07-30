using System.Text.Json;
using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Application.Features.UserModuleAccess.Dtos;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.BulkUpdateModuleAccess;

using UserModuleAccessEntity = CmScheme.Registration.Core.Entities.UserModuleAccess;

public sealed class BulkUpdateModuleAccessCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<BulkUpdateModuleAccessCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        BulkUpdateModuleAccessCommand request,
        CancellationToken cancellationToken)
    {
        Core.Entities.UserAccount? user = await dbContext.UserAccounts
            .FirstOrDefaultAsync(ua => ua.UserAccountId == request.UserAccountId, cancellationToken);

        if (user is null)
        {
            return Result<int>.NotFound("User account not found.");
        }

        List<UserModuleAccessEntity> existingAccesses = await dbContext.UserModuleAccesses
            .Where(uma => uma.UserAccountId == request.UserAccountId && uma.IsActive)
            .ToListAsync(cancellationToken);

        foreach (UserModuleAccessEntity existing in existingAccesses)
        {
            existing.IsActive = false;
        }

        int affectedCount = 0;

        foreach (ModuleAccessItemDto item in request.Accesses)
        {
            UserModuleAccessEntity? existingAccess = existingAccesses
                .FirstOrDefault(e => e.ModuleMasterId == item.ModuleMasterId && e.DivisionId == item.DivisionId && e.DistrictId == item.DistrictId && e.BlockId == item.BlockId);

            string oldValues = string.Empty;
            string newValues = JsonSerializer.Serialize(new
            {
                item.ModuleMasterId,
                item.CanRead,
                item.CanWrite,
                item.CanApprove,
                item.CanExport,
                item.DivisionId,
                item.DistrictId,
                item.BlockId
            });

            if (existingAccess is not null)
            {
                oldValues = JsonSerializer.Serialize(new
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

                existingAccess.CanRead = item.CanRead;
                existingAccess.CanWrite = item.CanWrite;
                existingAccess.CanApprove = item.CanApprove;
                existingAccess.CanExport = item.CanExport;
                existingAccess.DivisionId = item.DivisionId;
                existingAccess.DistrictId = item.DistrictId;
                existingAccess.BlockId = item.BlockId;
                existingAccess.IsActive = true;

                dbContext.ModuleAccessAuditLogs.Add(new ModuleAccessAuditLog
                {
                    UserModuleAccessId = existingAccess.UserModuleAccessId,
                    UserAccountId = request.UserAccountId,
                    ModuleMasterId = item.ModuleMasterId,
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
                    ModuleMasterId = item.ModuleMasterId,
                    CanRead = item.CanRead,
                    CanWrite = item.CanWrite,
                    CanApprove = item.CanApprove,
                    CanExport = item.CanExport,
                    DivisionId = item.DivisionId,
                    DistrictId = item.DistrictId,
                    BlockId = item.BlockId,
                    IsActive = true,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = request.PerformedBy
                };

                dbContext.UserModuleAccesses.Add(newAccess);
                await dbContext.SaveChangesAsync(cancellationToken);

                dbContext.ModuleAccessAuditLogs.Add(new ModuleAccessAuditLog
                {
                    UserModuleAccessId = newAccess.UserModuleAccessId,
                    UserAccountId = request.UserAccountId,
                    ModuleMasterId = item.ModuleMasterId,
                    Action = "GRANT",
                    OldValues = null,
                    NewValues = newValues,
                    PerformedBy = request.PerformedBy,
                    PerformedOn = DateTime.UtcNow
                });
            }

            affectedCount++;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(affectedCount);
    }
}
