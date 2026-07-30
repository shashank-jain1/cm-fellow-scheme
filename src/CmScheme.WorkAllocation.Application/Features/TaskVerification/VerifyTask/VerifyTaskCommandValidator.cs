using FluentValidation;

namespace CmScheme.WorkAllocation.Application.Features.TaskVerification.VerifyTask;

public sealed class VerifyTaskCommandValidator : AbstractValidator<VerifyTaskCommand>
{
    public VerifyTaskCommandValidator()
    {
        RuleFor(x => x.WorkAllocationId)
            .GreaterThan(0).WithMessage("WorkAllocationId must be greater than 0.");
        RuleFor(x => x.VerificationStatus)
            .NotEmpty().WithMessage("VerificationStatus is required.")
            .Must(s => s == "Pending" || s == "Approved" || s == "Rejected")
            .WithMessage("VerificationStatus must be Pending, Approved, or Rejected.");
        RuleFor(x => x.Comments)
            .MaximumLength(500).WithMessage("Comments must not exceed 500 characters.");
    }
}
