using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.GetTaskProgressById;

public sealed record GetTaskProgressByIdQuery : IQuery<Result<Core.Dtos.TaskProgressDto?>>
{
    public int TaskProgressId { get; init; }
}
