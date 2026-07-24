using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.Masters.Application.Features.Projects.CreateProject;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Projects;

public sealed class CreateProject
{
    public static async Task<IResult> Create(CreateProjectCommand command, ISender sender, CancellationToken ct)
    {
        ValueTask<Result<int>> result = sender.Send(command, ct);
        return await result.ToApiResultAsync();
    }
}
