using FluentValidation;

namespace CmScheme.Certificate.Application.Features.Certificate.GenerateCertificate;

public sealed class GenerateCertificateCommandValidator : AbstractValidator<GenerateCertificateCommand>
{
    public GenerateCertificateCommandValidator()
    {
        RuleFor(x => x.CertificateId)
            .GreaterThan(0).WithMessage("Certificate ID is required.");
    }
}
