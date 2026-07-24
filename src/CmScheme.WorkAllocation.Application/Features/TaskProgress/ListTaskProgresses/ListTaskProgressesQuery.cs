using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.ListTaskProgresses;

public sealed record ListTaskProgressesQuery(int WorkAllocationId) : IQuery<Result<IReadOnlyList<Core.Dtos.TaskProgressDto>>>;
