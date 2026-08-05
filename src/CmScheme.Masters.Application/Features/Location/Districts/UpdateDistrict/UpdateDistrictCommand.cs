using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Districts.UpdateDistrict;

public sealed record UpdateDistrictCommand : ICommand<Result>
{
    public int DistrictId { get; init; }
    public int DivisionId { get; init; }
    public string DistrictName { get; init; } = null!;
    public string? DistrictCode { get; init; }
}
