using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Divisions.DeleteDivision;

public sealed record DeleteDivisionCommand : ICommand<Result>
{
    public int DivisionId { get; init; }
}
