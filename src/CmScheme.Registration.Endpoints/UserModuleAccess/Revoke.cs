using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Registration.Application.Features.UserModuleAccess.RevokeModuleAccess;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.UserModuleAccess;

public sealed class Revoke
{
    public static async Task<IResult> Handle(
        int userModuleAccessId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        RevokeModuleAccessCommand command = new RevokeModuleAccessCommand
        {
            UserModuleAccessId = userModuleAccessId,
            PerformedBy = 0
        };

        ValueTask<Result<bool>> result = sender.Send(command, cancellationToken);
        return await result.ToApiResultAsync();
    }
}
