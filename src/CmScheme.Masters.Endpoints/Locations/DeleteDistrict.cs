using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.Districts.DeleteDistrict;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class DeleteDistrict
{
    public static async Task<IResult> Delete(int id, ISender sender, CancellationToken ct)
    {
        Result result = await sender.Send(new DeleteDistrictCommand { DistrictId = id }, ct);
        return result.IsSuccess ? Results.NoContent() : Results.NotFound();
    }
}
