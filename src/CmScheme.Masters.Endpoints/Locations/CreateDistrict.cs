using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Location.Districts.CreateDistrict;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class CreateDistrict
{
    public static async Task<IResult> Create(CreateDistrictCommand command, ISender sender, CancellationToken ct)
    {
        ValueTask<Result<int>> result = sender.Send(command, ct);
        return await result.ToApiResultAsync();
    }
}
