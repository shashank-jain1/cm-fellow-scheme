using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Blocks.DeleteBlock;

public sealed record DeleteBlockCommand : ICommand<Result>
{
    public int BlockId { get; init; }
}
