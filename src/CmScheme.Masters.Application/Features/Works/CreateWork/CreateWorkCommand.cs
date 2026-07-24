using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Works.CreateWork;

public sealed record CreateWorkCommand(
    int ProjectId,
    string WorkName,
    string? WorkDescription,
    string Priority,
    DateTime StartDate,
    DateTime EndDate,
    string AssignedTo,
    string? Remarks) : ICommand<Result<int>>;
