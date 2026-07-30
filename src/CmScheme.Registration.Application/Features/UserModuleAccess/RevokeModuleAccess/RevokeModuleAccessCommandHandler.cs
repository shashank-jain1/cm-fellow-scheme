using System.Text.Json;
using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.RevokeModuleAccess;

using UserModuleAccessEntity = CmScheme.Registration.Core.Entities.UserModuleAccess;

public sealed class RevokeModuleAccessCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<RevokeModuleAccessCommand, Result<bool>>
{
    public async ValueTask<Result<bool>> Handle(
        RevokeModuleAccessCommand request,
        CancellationToken cancellationToken)
    {
        UserModuleAccessEntity? access = await dbContext.UserModuleAccesses
            .FirstOrDefaultAsync(uma => uma.UserModuleAccessId == request.UserModuleAccessId, cancellationToken);

        if (access is null)
        {
            return Result<bool>.NotFound("Module access record not found.");
        }

        string oldValues = JsonSerializer.Serialize(new
        {
            access.ModuleMasterId,
            access.CanRead,
            access.CanWrite,
            access.CanApprove,
            access.CanExport,
            access.DivisionId,
            access.DistrictId,
            access.BlockId,
            access.IsActive
        });

        access.IsActive = false;

        dbContext.ModuleAccessAuditLogs.Add(new ModuleAccessAuditLog
        {
            UserModuleAccessId = access.UserModuleAccessId,
            UserAccountId = access.UserAccountId,
            ModuleMasterId = access.ModuleMasterId,
            Action = "REVOKE",
            OldValues = oldValues,
            NewValues = null,
            PerformedBy = request.PerformedBy,
            PerformedOn = DateTime.UtcNow
        });

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
