using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Districts.ListDistrictsByDivision;

public sealed record ListDistrictsByDivisionQuery : IQuery<Result<List<Core.Dtos.DistrictDto>>>
{
    public int DivisionId { get; init; }
}
