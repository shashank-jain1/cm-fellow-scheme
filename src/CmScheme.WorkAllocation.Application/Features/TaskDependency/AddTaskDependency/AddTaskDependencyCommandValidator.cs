using FluentValidation;

namespace CmScheme.WorkAllocation.Application.Features.TaskDependency.AddTaskDependency;

public sealed class AddTaskDependencyCommandValidator : AbstractValidator<AddTaskDependencyCommand>
{
    public AddTaskDependencyCommandValidator()
    {
        RuleFor(x => x.WorkAllocationId)
            .GreaterThan(0).WithMessage("WorkAllocationId must be greater than 0.");
    }
}
