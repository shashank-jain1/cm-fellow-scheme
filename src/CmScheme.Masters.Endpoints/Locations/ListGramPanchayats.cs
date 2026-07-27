using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.GramPanchayats.ListGramPanchayatsByBlock;
using CmScheme.Masters.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class ListGramPanchayats
{
    public static async Task<IResult> List(int? blockId, ISender sender, CancellationToken ct)
    {
        ValueTask<Result<List<GramPanchayatDto>>> result = sender.Send(
            new ListGramPanchayatsByBlockQuery { BlockId = blockId }, ct);
        return await result.ToApiResultAsync();
    }
}
