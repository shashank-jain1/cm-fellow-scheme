using Ardalis.Result;
using Mediator;
using CmScheme.Masters.Application.Features.Works.CreateWork;

namespace CmScheme.Masters.Application.Features.Works.GetWorkById;

public sealed record GetWorkByIdQuery : IQuery<Result<CreateWorkCommand>>
{
    public int WorkId { get; init; }
}
