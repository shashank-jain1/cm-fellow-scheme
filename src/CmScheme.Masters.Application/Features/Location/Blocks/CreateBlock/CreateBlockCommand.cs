using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Blocks.CreateBlock;

public sealed record CreateBlockCommand : ICommand<Result<int>>
{
    public int DistrictId { get; init; }
    public string BlockName { get; init; } = null!;
    public string? BlockCode { get; init; }
}
