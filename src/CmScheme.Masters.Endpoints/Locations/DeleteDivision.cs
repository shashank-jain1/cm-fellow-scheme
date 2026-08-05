using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.Divisions.DeleteDivision;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class DeleteDivision
{
    public static async Task<IResult> Delete(int id, ISender sender, CancellationToken ct)
    {
        Result result = await sender.Send(new DeleteDivisionCommand { DivisionId = id }, ct);
        return result.IsSuccess ? Results.NoContent() : Results.NotFound();
    }
}
