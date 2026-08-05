using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Divisions.UpdateDivision;

public sealed record UpdateDivisionCommand : ICommand<Result>
{
    public int DivisionId { get; init; }
    public int StateId { get; init; }
    public string DivisionName { get; init; } = null!;
    public string? DivisionCode { get; init; }
}
