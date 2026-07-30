using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Leave.CheckConflict;

public sealed record CheckLeaveConflictQuery : IQuery<Result<LeaveConflictResult>>
{
    public int UserAccountId { get; init; }
    public int LeaveTypeId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int? ExcludeApplicationId { get; init; }
}

public sealed record LeaveConflictResult
{
    public bool HasConflict { get; init; }
    public List<ConflictingApplicationDto> ConflictingApplications { get; init; } = [];
}

public sealed record ConflictingApplicationDto
{
    public int LeaveApplicationId { get; init; }
    public string ApplicationNumber { get; init; } = null!;
    public string LeaveTypeName { get; init; } = null!;
    public DateTime FromDate { get; init; }
    public DateTime ToDate { get; init; }
    public decimal NumberOfDays { get; init; }
    public string Status { get; init; } = null!;
}
