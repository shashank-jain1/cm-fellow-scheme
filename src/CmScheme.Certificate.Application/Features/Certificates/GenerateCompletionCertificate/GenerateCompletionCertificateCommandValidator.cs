using FluentValidation;

namespace CmScheme.Certificate.Application.Features.Certificates.GenerateCompletionCertificate;

public sealed class GenerateCompletionCertificateCommandValidator : AbstractValidator<GenerateCompletionCertificateCommand>
{
    public GenerateCompletionCertificateCommandValidator()
    {
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("Applicant ID is required.");
    }
}
