using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Projects.ListProjects;
using CmScheme.Masters.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Projects;

public sealed class ListProjects
{
    public static async Task<IResult> List(ISender sender, CancellationToken ct)
    {
        ValueTask<Result<List<ProjectDto>>> result = sender.Send(new ListProjectsQuery(), ct);
        return await result.ToApiResultAsync();
    }
}
