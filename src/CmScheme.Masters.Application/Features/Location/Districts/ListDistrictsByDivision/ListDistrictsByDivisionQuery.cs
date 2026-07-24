using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Districts.ListDistrictsByDivision;

public sealed record ListDistrictsByDivisionQuery(int DivisionId) : IQuery<Result<List<Core.Dtos.DistrictDto>>>;
