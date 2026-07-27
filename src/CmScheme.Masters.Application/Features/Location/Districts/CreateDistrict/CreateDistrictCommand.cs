using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Districts.CreateDistrict;

public sealed record CreateDistrictCommand : ICommand<Result<int>>
{
    public int DivisionId { get; init; }
    public string DistrictName { get; init; } = null!;
    public string? DistrictCode { get; init; }
}
