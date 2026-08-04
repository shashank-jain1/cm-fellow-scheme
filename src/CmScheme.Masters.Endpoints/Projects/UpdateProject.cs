using Ardalis.Result;
using CmScheme.Masters.Application.Features.Projects.UpdateProject;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace CmScheme.Masters.Endpoints.Projects;

public static class UpdateProject
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Update(
        int id,
        UpdateProjectCommand command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        if (id != command.ProjectId)
        {
            return Results.BadRequest("Route ID does not match command ProjectId.");
        }

        Result result = await mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
    }
}
