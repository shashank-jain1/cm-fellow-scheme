using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Registration.Application.Features.ModuleMaster.GetAuditLog;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.UserModuleAccess;

public sealed class GetAuditLogEndpoint
{
    public static async Task<IResult> Handle(
        int? userAccountId,
        int? moduleMasterId,
        int pageSize,
        int pageNumber,
        ISender sender,
        CancellationToken cancellationToken)
    {
        ValueTask<Result<List<AuditLogDto>>> result = sender.Send(
            new GetAuditLogQuery
            {
                UserAccountId = userAccountId,
                ModuleMasterId = moduleMasterId,
                PageSize = pageSize > 0 ? pageSize : 50,
                PageNumber = pageNumber > 0 ? pageNumber : 1,
            },
            cancellationToken);

        return await result.ToApiResultAsync();
    }
}
