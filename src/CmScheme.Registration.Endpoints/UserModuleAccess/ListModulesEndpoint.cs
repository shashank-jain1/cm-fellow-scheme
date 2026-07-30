using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Registration.Application.Features.ModuleMaster.ListModules;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.UserModuleAccess;

public sealed class ListModulesEndpoint
{
    public static async Task<IResult> Handle(
        ISender sender,
        CancellationToken cancellationToken)
    {
        ValueTask<Result<List<ListModulesResult>>> result = sender.Send(
            new ListModulesQuery(),
            cancellationToken);

        return await result.ToApiResultAsync();
    }
}
