using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Divisions.CreateDivision;

public sealed record CreateDivisionCommand : ICommand<Result<int>>
{
    public int StateId { get; init; }
    public string DivisionName { get; init; } = null!;
    public string? DivisionCode { get; init; }
}
