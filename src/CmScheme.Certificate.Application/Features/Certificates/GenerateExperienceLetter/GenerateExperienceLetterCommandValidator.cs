using FluentValidation;

namespace CmScheme.Certificate.Application.Features.Certificates.GenerateExperienceLetter;

public sealed class GenerateExperienceLetterCommandValidator : AbstractValidator<GenerateExperienceLetterCommand>
{
    public GenerateExperienceLetterCommandValidator()
    {
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("Applicant ID is required.");

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate).WithMessage("Start date must be before end date.");

        RuleFor(x => x.EndDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("End date cannot be in the future.");

        RuleFor(x => x.SupervisorName)
            .NotEmpty().WithMessage("Supervisor name is required.")
            .MaximumLength(200).WithMessage("Supervisor name must not exceed 200 characters.");
    }
}
