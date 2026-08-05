using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.States.DeleteState;

public sealed record DeleteStateCommand : ICommand<Result>
{
    public int StateId { get; init; }
}
