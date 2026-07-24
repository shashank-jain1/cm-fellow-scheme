using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.States.ListStates;
using CmScheme.Masters.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class ListStates
{
    public static async Task<IResult> List(ISender sender, CancellationToken ct)
    {
        ValueTask<Result<List<StateDto>>> result = sender.Send(new ListStatesQuery(), ct);
        return await result.ToApiResultAsync();
    }
}
