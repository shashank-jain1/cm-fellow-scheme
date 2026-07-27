using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.States.CreateState;

public sealed record CreateStateCommand : ICommand<Result<int>>
{
    public string StateName { get; init; } = null!;
    public string StateCode { get; init; } = null!;
    public string? StateShortName { get; init; }
    public int? DisplayOrder { get; init; }
}
