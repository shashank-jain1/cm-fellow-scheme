using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.States.DeleteState;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class DeleteState
{
    public static async Task<IResult> Delete(int id, ISender sender, CancellationToken ct)
    {
        Result result = await sender.Send(new DeleteStateCommand { StateId = id }, ct);
        return result.IsSuccess ? Results.NoContent() : Results.NotFound();
    }
}
