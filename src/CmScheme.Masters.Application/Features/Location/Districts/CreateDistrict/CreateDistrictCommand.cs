using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Districts.CreateDistrict;

public sealed record CreateDistrictCommand(
    int DivisionId,
    string DistrictName,
    string? DistrictCode) : ICommand<Result<int>>;
