using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.Divisions.UpdateDivision;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class UpdateDivision
{
    public static async Task<IResult> Update(int id, UpdateDivisionCommand command, ISender sender, CancellationToken ct)
    {
        if (id != command.DivisionId) return Results.BadRequest("Route ID does not match command DivisionId.");
        Result result = await sender.Send(command, ct);
        return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
    }
}
