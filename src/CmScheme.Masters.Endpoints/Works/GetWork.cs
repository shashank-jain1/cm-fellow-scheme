using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Works.CreateWork;
using CmScheme.Masters.Application.Features.Works.GetWorkById;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Works;

public sealed class GetWork
{
    public static async Task<IResult> GetById(int id, ISender sender, CancellationToken ct)
    {
        Result<CreateWorkCommand> result = await sender.Send(new GetWorkByIdQuery { WorkId = id }, ct);
        return result.ToApiResult();
    }
}
