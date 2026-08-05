using FluentValidation;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.AssignWorkAllocation;

public sealed class AssignWorkAllocationCommandValidator : AbstractValidator<AssignWorkAllocationCommand>
{
    public AssignWorkAllocationCommandValidator()
    {
        RuleFor(x => x.WorkAllocationId)
            .GreaterThan(0).WithMessage("WorkAllocationId must be greater than 0.");
    }
}
