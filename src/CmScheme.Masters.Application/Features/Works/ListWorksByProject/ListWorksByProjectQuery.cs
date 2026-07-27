using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Works.ListWorksByProject;

public sealed record ListWorksByProjectQuery : IQuery<Result<List<Core.Dtos.WorkDto>>>
{
    public int ProjectId { get; init; }
}
