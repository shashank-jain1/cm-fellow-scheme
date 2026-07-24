using FluentValidation;

namespace CmScheme.Certificate.Application.Features.Exit.SubmitExitReadiness;

public sealed class SubmitExitReadinessCommandValidator : AbstractValidator<SubmitExitReadinessCommand>
{
    public SubmitExitReadinessCommandValidator()
    {
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("ApplicantId must be greater than 0.");
        RuleFor(x => x.CompletionStatus)
            .NotEmpty().WithMessage("CompletionStatus is required.")
            .MaximumLength(50).WithMessage("CompletionStatus must not exceed 50 characters.");
        RuleFor(x => x.VerificationFlags)
            .NotEmpty().WithMessage("VerificationFlags is required.")
            .MaximumLength(500).WithMessage("VerificationFlags must not exceed 500 characters.");
        RuleFor(x => x.CreatedBy)
            .NotEmpty().WithMessage("CreatedBy is required.")
            .MaximumLength(200).WithMessage("CreatedBy must not exceed 200 characters.");
    }
}
