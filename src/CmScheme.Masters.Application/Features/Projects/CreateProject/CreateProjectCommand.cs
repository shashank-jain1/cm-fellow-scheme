using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Projects.CreateProject;

public sealed record CreateProjectCommand(
    string ProjectName,
    string ProjectCode,
    string? ProjectDescription,
    string DepartmentName,
    DateTime StartDate,
    DateTime EndDate,
    string ProjectIncharge,
    decimal? BudgetAmount,
    int BudgetApprovedBy,
    string? ProjectDocumentPath) : ICommand<Result<int>>;
