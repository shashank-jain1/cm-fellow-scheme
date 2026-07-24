using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Works.CreateWork;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Works;

public sealed class CreateWork
{
    public static async Task<IResult> Create(CreateWorkCommand command, ISender sender, CancellationToken ct)
    {
        ValueTask<Result<int>> result = sender.Send(command, ct);
        return await result.ToApiResultAsync();
    }
}
