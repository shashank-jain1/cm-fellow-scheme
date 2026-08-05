using FluentValidation;

namespace CmScheme.WorkAllocation.Application.Features.WorkAllocation.DeactivateWorkAllocation;

public sealed class DeactivateWorkAllocationCommandValidator : AbstractValidator<DeactivateWorkAllocationCommand>
{
    public DeactivateWorkAllocationCommandValidator()
    {
        RuleFor(x => x.WorkAllocationId)
            .GreaterThan(0).WithMessage("WorkAllocationId must be greater than 0.");
    }
}
