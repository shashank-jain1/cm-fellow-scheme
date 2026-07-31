using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Registration.Core.Dtos;
using CmScheme.Registration.Application.Features.UserModuleAccess.GetUserModuleAccess;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.UserModuleAccess;

public sealed class GetUserAccess
{
    public static async Task<IResult> Handle(
        int userAccountId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        ValueTask<Result<List<ModuleAccessDto>>> result = sender.Send(
            new GetUserModuleAccessQuery { UserAccountId = userAccountId },
            cancellationToken);

        return await result.ToApiResultAsync();
    }
}
