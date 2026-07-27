using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Works.ListWorksByProject;
using CmScheme.Masters.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Works;

public sealed class ListWorks
{
    public static async Task<IResult> List(int projectId, ISender sender, CancellationToken ct)
    {
        ValueTask<Result<List<WorkDto>>> result = sender.Send(new ListWorksByProjectQuery { ProjectId = projectId }, ct);
        return await result.ToApiResultAsync();
    }
}
