using FluentValidation;

namespace CmScheme.Registration.Application.Features.Registration.ApproveRegistration;

public sealed class ApproveRegistrationCommandValidator : AbstractValidator<ApproveRegistrationCommand>
{
    public ApproveRegistrationCommandValidator()
    {
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("Applicant ID must be greater than zero.");

        RuleFor(x => x.ApprovedBy)
            .GreaterThan(0).WithMessage("Approved by must be greater than zero.");
    }
}
