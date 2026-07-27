using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Projects.CreateProject;

public sealed record CreateProjectCommand : ICommand<Result<int>>
{
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
}
