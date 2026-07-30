using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Registration.Application.Features.UserModuleAccess.GrantModuleAccess;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.UserModuleAccess;

public sealed class Grant
{
    public static async Task<IResult> Handle(
        GrantAccessRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        GrantModuleAccessCommand command = new GrantModuleAccessCommand
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
            PerformedBy = request.PerformedBy
        };

        ValueTask<Result<int>> result = sender.Send(command, cancellationToken);
        return await result.ToApiResultAsync();
    }
}

public sealed record GrantAccessRequest(
    int UserAccountId,
    int ModuleMasterId,
    bool CanRead = true,
    bool CanWrite = false,
    bool CanApprove = false,
    bool CanExport = false,
    int? DivisionId = null,
    int? DistrictId = null,
    int? BlockId = null,
    int PerformedBy = 0);
