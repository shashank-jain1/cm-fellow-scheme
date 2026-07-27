using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Blocks.ListBlocksByDistrict;

public sealed record ListBlocksByDistrictQuery : IQuery<Result<List<Core.Dtos.BlockDto>>>
{
    public int? DistrictId { get; init; }
}
