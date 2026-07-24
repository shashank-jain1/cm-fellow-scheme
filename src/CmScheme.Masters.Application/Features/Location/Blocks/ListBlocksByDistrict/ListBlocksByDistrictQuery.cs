using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Blocks.ListBlocksByDistrict;

public sealed record ListBlocksByDistrictQuery(int DistrictId) : IQuery<Result<List<Core.Dtos.BlockDto>>>;
