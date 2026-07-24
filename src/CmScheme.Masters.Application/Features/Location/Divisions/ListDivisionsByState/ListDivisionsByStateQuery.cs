using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.Divisions.ListDivisionsByState;

public sealed record ListDivisionsByStateQuery(int StateId) : IQuery<Result<List<Core.Dtos.DivisionDto>>>;
