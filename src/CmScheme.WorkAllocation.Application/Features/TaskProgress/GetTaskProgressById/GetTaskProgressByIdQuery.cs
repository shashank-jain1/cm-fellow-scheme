using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.GetTaskProgressById;

public sealed record GetTaskProgressByIdQuery(int TaskProgressId) : IQuery<Result<Core.Dtos.TaskProgressDto?>>;
