using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.GramPanchayats.DeleteGramPanchayat;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class DeleteGramPanchayat
{
    public static async Task<IResult> Delete(int id, ISender sender, CancellationToken ct)
    {
        Result result = await sender.Send(new DeleteGramPanchayatCommand { GramPanchayatId = id }, ct);
        return result.IsSuccess ? Results.NoContent() : Results.NotFound();
    }
}
