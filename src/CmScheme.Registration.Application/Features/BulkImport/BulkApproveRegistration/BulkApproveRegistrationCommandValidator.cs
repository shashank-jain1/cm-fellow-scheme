using FluentValidation;

namespace CmScheme.Registration.Application.Features.Registration.BulkApprove;

public sealed class BulkApproveRegistrationCommandValidator : AbstractValidator<BulkApproveRegistrationCommand>
{
    public BulkApproveRegistrationCommandValidator()
    {
        RuleFor(x => x.ApplicantIds)
            .NotEmpty().WithMessage("ApplicantIds is required.");
    }
}
