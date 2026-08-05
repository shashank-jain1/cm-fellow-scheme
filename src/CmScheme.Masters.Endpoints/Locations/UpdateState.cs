using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.States.UpdateState;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class UpdateState
{
    public static async Task<IResult> Update(int id, UpdateStateCommand command, ISender sender, CancellationToken ct)
    {
        if (id != command.StateId) return Results.BadRequest("Route ID does not match command StateId.");
        Result result = await sender.Send(command, ct);
        return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
    }
}
