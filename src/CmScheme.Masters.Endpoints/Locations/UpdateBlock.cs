using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.Blocks.UpdateBlock;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class UpdateBlock
{
    public static async Task<IResult> Update(int id, UpdateBlockCommand command, ISender sender, CancellationToken ct)
    {
        if (id != command.BlockId) return Results.BadRequest("Route ID does not match command BlockId.");
        Result result = await sender.Send(command, ct);
        return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
    }
}
