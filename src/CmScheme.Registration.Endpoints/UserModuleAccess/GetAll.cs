using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Registration.Core.Dtos;
using CmScheme.Registration.Application.Features.UserModuleAccess.GetAllModuleAccess;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Registration.Endpoints.UserModuleAccess;

public sealed class GetAll
{
    public static async Task<IResult> Handle(
        ISender sender,
        CancellationToken cancellationToken)
    {
        ValueTask<Result<List<UserAccessSummaryDto>>> result = sender.Send(
            new GetAllModuleAccessQuery(),
            cancellationToken);

        return await result.ToApiResultAsync();
    }
}
