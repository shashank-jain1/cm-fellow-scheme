using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.Districts.ListDistrictsByDivision;
using CmScheme.Masters.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class ListDistricts
{
    public static async Task<IResult> List(int? divisionId, ISender sender, CancellationToken ct)
    {
        ValueTask<Result<List<DistrictDto>>> result = sender.Send(
            new ListDistrictsByDivisionQuery { DivisionId = divisionId }, ct);
        return await result.ToApiResultAsync();
    }
}
