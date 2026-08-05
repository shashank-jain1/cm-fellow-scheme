using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.States.UpdateState;

public sealed record UpdateStateCommand : ICommand<Result>
{
    public int StateId { get; init; }
    public string StateName { get; init; } = null!;
    public string StateCode { get; init; } = null!;
    public string? StateShortName { get; init; }
    public int? DisplayOrder { get; init; }
    public bool IsActive { get; init; }
}
