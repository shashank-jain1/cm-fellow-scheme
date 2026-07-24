using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.States.CreateState;

public sealed record CreateStateCommand(
    string StateName,
    string StateCode,
    string? StateShortName,
    int? DisplayOrder) : ICommand<Result<int>>;
