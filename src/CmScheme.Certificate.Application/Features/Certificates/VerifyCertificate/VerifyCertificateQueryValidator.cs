using FluentValidation;

namespace CmScheme.Certificate.Application.Features.Certificates.VerifyCertificate;

public sealed class VerifyCertificateQueryValidator : AbstractValidator<VerifyCertificateQuery>
{
    public VerifyCertificateQueryValidator()
    {
        RuleFor(x => x.CertificateNumber)
            .NotEmpty().WithMessage("Certificate number is required.")
            .MaximumLength(100).WithMessage("Certificate number must not exceed 100 characters.");
    }
}
