using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.UpdateWorkAllocation;

public sealed record UpdateWorkAllocationCommand(
    int WorkAllocationId,
    int ProjectId,
    string WorkProjectId,
    string WorkDescription,
    string Priority,
    DateTime StartDate,
    DateTime? EndDate,
    int DurationDays,
    int SurveysPerIntern,
    int DivisionId,
    int DistrictId,
    int BlockId,
    bool ActiveStatus,
    string Status,
    string ModifiedBy
) : ICommand<Result<bool>>;
