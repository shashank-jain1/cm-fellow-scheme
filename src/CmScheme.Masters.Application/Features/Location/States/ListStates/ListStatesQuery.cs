using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Location.States.ListStates;

public sealed record ListStatesQuery : IQuery<Result<List<Core.Dtos.StateDto>>>;
