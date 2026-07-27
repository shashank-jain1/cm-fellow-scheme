using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.LookupMaster.ListLookupMasters;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.LookupMasters;

public sealed class List
{
    public static async Task<IResult> Handle(
        string? masterType,
        ISender sender,
        CancellationToken ct)
    {
        ValueTask<Result<IReadOnlyList<LookupMasterListItem>>> result = sender.Send(
            new ListLookupMastersQuery { MasterType = masterType },
            ct);

        return await result.ToApiResultAsync();
    }
}
