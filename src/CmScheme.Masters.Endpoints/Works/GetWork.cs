using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Works.GetWorkById;
using CmScheme.Masters.Application.Features.Works.CreateWork;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Works;

public sealed class GetWork
{
    public static async Task<IResult> GetById(int workId, ISender sender, CancellationToken ct)
    {
        GetWorkByIdQuery query = new GetWorkByIdQuery { WorkId = workId };
        ValueTask<Result<CreateWorkCommand>> result = sender.Send(query, ct);
        return await result.ToApiResultAsync();
    }
}
