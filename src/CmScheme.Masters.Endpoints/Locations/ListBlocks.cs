using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.Blocks.ListBlocksByDistrict;
using CmScheme.Masters.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class ListBlocks
{
    public static async Task<IResult> List(int? districtId, ISender sender, CancellationToken ct)
    {
        ValueTask<Result<List<BlockDto>>> result = sender.Send(
            new ListBlocksByDistrictQuery { DistrictId = districtId }, ct);
        return await result.ToApiResultAsync();
    }
}
