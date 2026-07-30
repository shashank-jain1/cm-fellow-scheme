using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.UpdateTaskProgress;

public sealed record UpdateTaskProgressCommand : ICommand<Result<int>>
{
    public int WorkAllocationId { get; init; }
    public string ProgressNotes { get; init; } = null!;
    public int ProgressPercentage { get; init; }
    public string Status { get; init; } = null!;
}
