using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Registration.Application.Features.UserModuleAccess.BulkUpdateModuleAccess;
using CmScheme.Registration.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.UserModuleAccess;

public sealed class BulkUpdate
{
    public static async Task<IResult> Handle(
        BulkUpdateRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        BulkUpdateModuleAccessCommand command = new BulkUpdateModuleAccessCommand
        {
            UserAccountId = request.UserAccountId,
            Accesses = request.Accesses,
            PerformedBy = request.PerformedBy
        };

        ValueTask<Result<int>> result = sender.Send(command, cancellationToken);
        return await result.ToApiResultAsync();
    }
}

public sealed record BulkUpdateRequest(
    int UserAccountId,
    List<ModuleAccessItemDto> Accesses,
    int PerformedBy);
