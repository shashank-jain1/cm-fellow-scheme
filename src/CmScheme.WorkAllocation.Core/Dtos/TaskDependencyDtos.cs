namespace CmScheme.WorkAllocation.Core.Dtos;

public sealed record TaskDependencyDto(
    int TaskDependencyId,
    int WorkAllocationId,
    int DependsOnWorkAllocationId,
    DateTime CreatedOn);
