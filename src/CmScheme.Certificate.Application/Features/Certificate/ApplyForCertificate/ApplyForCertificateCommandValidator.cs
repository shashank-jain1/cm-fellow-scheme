using FluentValidation;

namespace CmScheme.Certificate.Application.Features.Certificate.ApplyForCertificate;

public sealed class ApplyForCertificateCommandValidator : AbstractValidator<ApplyForCertificateCommand>
{
    public ApplyForCertificateCommandValidator()
    {
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("ApplicantId must be greater than 0.");
        RuleFor(x => x.ApplicantName)
            .NotEmpty().WithMessage("ApplicantName is required.")
            .MaximumLength(200).WithMessage("ApplicantName must not exceed 200 characters.");
        RuleFor(x => x.ProgramName)
            .NotEmpty().WithMessage("ProgramName is required.")
            .MaximumLength(200).WithMessage("ProgramName must not exceed 200 characters.");
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required.");
        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("EndDate is required.")
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("EndDate must be on or after StartDate.");
        RuleFor(x => x.DurationDays)
            .GreaterThan(0).WithMessage("DurationDays must be greater than 0.");
        RuleFor(x => x.CreatedBy)
            .NotEmpty().WithMessage("CreatedBy is required.")
            .MaximumLength(200).WithMessage("CreatedBy must not exceed 200 characters.");
    }
}
