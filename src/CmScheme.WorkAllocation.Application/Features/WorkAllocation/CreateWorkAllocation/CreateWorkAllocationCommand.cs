using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.CreateWorkAllocation;

public sealed record CreateWorkAllocationCommand : ICommand<Result<int>>
{
    public int ProjectId { get; init; }
    public string WorkProjectId { get; init; } = null!;
    public string WorkDescription { get; init; } = null!;
    public string Priority { get; init; } = null!;
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int DurationDays { get; init; }
    public int SurveysPerIntern { get; init; }
    public int DivisionId { get; init; }
    public int DistrictId { get; init; }
    public int BlockId { get; init; }
    public bool ActiveStatus { get; init; }
    public string Status { get; init; } = null!;
    public string CreatedBy { get; init; } = null!;
}
