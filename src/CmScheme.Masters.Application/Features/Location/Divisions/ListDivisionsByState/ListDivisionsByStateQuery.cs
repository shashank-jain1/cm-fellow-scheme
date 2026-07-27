using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Divisions.ListDivisionsByState;

public sealed record ListDivisionsByStateQuery : IQuery<Result<List<Core.Dtos.DivisionDto>>>
{
    public int? StateId { get; init; }
}
