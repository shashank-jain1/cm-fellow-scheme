using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Blocks.UpdateBlock;

public sealed record UpdateBlockCommand : ICommand<Result>
{
    public int BlockId { get; init; }
    public int DistrictId { get; init; }
    public string BlockName { get; init; } = null!;
    public string? BlockCode { get; init; }
}
