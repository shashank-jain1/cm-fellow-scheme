using Ardalis.Result;
using CmScheme.Masters.Application.Features.Projects.DeleteProject;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Masters.Endpoints.Projects;

public static class DeleteProject
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Delete(
        int id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        Result result = await mediator.Send(new DeleteProjectCommand { ProjectId = id }, cancellationToken);
        return result.IsSuccess ? Results.NoContent() : Results.NotFound();
    }
}
