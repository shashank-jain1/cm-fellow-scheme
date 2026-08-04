using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Projects.UpdateProject;

public sealed record UpdateProjectCommand : ICommand<Result>
{
    public int ProjectId { get; init; }
    public string ProjectName { get; init; } = null!;
    public string ProjectCode { get; init; } = null!;
    public string? ProjectDescription { get; init; }
    public string DepartmentName { get; init; } = null!;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string ProjectIncharge { get; init; } = null!;
    public decimal? BudgetAmount { get; init; }
    public int BudgetApprovedBy { get; init; }
    public string? ProjectDocumentPath { get; init; }
    public bool IsActive { get; init; } = true;
}
