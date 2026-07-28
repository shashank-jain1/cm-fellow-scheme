using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Works.UpdateWork;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Works;

public sealed class UpdateWork
{
    public static async Task<IResult> Update(int workId, UpdateWorkCommand command, ISender sender, CancellationToken ct)
    {
        UpdateWorkCommand commandWithId = command with { WorkId = workId };
        ValueTask<Result> result = sender.Send(commandWithId, ct);
        return await result.ToApiResultAsync();
    }
}
