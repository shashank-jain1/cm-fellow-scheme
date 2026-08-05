using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.Districts.UpdateDistrict;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class UpdateDistrict
{
    public static async Task<IResult> Update(int id, UpdateDistrictCommand command, ISender sender, CancellationToken ct)
    {
        if (id != command.DistrictId) return Results.BadRequest("Route ID does not match command DistrictId.");
        Result result = await sender.Send(command, ct);
        return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
    }
}
