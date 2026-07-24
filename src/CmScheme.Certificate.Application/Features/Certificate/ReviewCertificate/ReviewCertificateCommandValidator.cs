using FluentValidation;

namespace CmScheme.Certificate.Application.Features.Certificate.ReviewCertificate;

public sealed class ReviewCertificateCommandValidator : AbstractValidator<ReviewCertificateCommand>
{
    public ReviewCertificateCommandValidator()
    {
        RuleFor(x => x.CertificateId)
            .GreaterThan(0).WithMessage("CertificateId must be greater than 0.");
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .MaximumLength(50).WithMessage("Status must not exceed 50 characters.");
        RuleFor(x => x.VerifiedBy)
            .NotEmpty().WithMessage("VerifiedBy is required.")
            .MaximumLength(200).WithMessage("VerifiedBy must not exceed 200 characters.");
    }
}
