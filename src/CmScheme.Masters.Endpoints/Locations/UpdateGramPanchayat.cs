using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.GramPanchayats.UpdateGramPanchayat;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class UpdateGramPanchayat
{
    public static async Task<IResult> Update(int id, UpdateGramPanchayatCommand command, ISender sender, CancellationToken ct)
    {
        if (id != command.GramPanchayatId) return Results.BadRequest("Route ID does not match command GramPanchayatId.");
        Result result = await sender.Send(command, ct);
        return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
    }
}
