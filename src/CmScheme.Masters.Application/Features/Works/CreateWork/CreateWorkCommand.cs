using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Works.CreateWork;

public sealed record CreateWorkCommand : ICommand<Result<int>>
{
    public int ProjectId { get; init; }
    public string WorkName { get; init; } = null!;
    public string? WorkDescription { get; init; }
    public string Priority { get; init; } = null!;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string AssignedTo { get; init; } = null!;
    public string? Remarks { get; init; }
}
