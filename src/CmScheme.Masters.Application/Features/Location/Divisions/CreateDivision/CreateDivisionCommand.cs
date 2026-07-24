using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Divisions.CreateDivision;

public sealed record CreateDivisionCommand(
    int StateId,
    string DivisionName,
    string? DivisionCode) : ICommand<Result<int>>;
