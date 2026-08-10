using Ardalis.Result;
using Mediator;

namespace CmScheme.Masters.Application.Features.Department.CreateDepartment;

public sealed record CreateDepartmentCommand : ICommand<Result<int>>
{
    public string DepartmentName { get; init; } = null!;
    public string? DepartmentCode { get; init; }
}
