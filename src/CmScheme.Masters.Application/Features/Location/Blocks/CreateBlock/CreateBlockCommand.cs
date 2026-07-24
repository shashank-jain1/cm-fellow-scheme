using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Blocks.CreateBlock;

public sealed record CreateBlockCommand(
    int DistrictId,
    string BlockName,
    string? BlockCode) : ICommand<Result<int>>;
