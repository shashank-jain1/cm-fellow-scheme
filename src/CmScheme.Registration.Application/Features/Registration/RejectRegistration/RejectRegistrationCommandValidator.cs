using FluentValidation;

namespace CmScheme.Registration.Application.Features.Registration.RejectRegistration;

public sealed class RejectRegistrationCommandValidator : AbstractValidator<RejectRegistrationCommand>
{
    public RejectRegistrationCommandValidator()
    {
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("Applicant ID must be greater than zero.");

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("Rejection reason is required.")
            .MaximumLength(500).WithMessage("Rejection reason must not exceed 500 characters.");

        RuleFor(x => x.RejectedBy)
            .GreaterThan(0).WithMessage("Rejected by must be greater than zero.");
    }
}
