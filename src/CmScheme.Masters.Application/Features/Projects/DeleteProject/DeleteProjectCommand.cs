using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Projects.DeleteProject;

public sealed record DeleteProjectCommand : ICommand<Result>
{
    public int ProjectId { get; init; }
}
