using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.Divisions.ListDivisionsByState;
using CmScheme.Masters.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class ListDivisions
{
    public static async Task<IResult> List(int stateId, ISender sender, CancellationToken ct)
    {
        ValueTask<Result<List<DivisionDto>>> result = sender.Send(new ListDivisionsByStateQuery(stateId), ct);
        return await result.ToApiResultAsync();
    }
}
