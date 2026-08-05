using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Districts.DeleteDistrict;

public sealed record DeleteDistrictCommand : ICommand<Result>
{
    public int DistrictId { get; init; }
}
