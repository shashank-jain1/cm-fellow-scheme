using FluentValidation;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.RecordSurveySubmission;

public sealed class RecordSurveySubmissionCommandValidator : AbstractValidator<RecordSurveySubmissionCommand>
{
    public RecordSurveySubmissionCommandValidator()
    {
        RuleFor(x => x.TaskProgressId)
            .GreaterThan(0).WithMessage("Task Progress ID is required.");
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("Applicant ID is required.");
        RuleFor(x => x.SurveyPersonName)
            .NotEmpty().WithMessage("Survey Person Name is required.")
            .MaximumLength(200).WithMessage("Survey Person Name must not exceed 200 characters.");
        RuleFor(x => x.MobileNumber)
            .NotEmpty().WithMessage("Mobile Number is required.")
            .Matches(@"^[0-9]{10}$").WithMessage("Mobile Number must be 10 digits.");
        RuleFor(x => x.PanchayatName)
            .NotEmpty().WithMessage("Panchayat Name is required.")
            .MaximumLength(150).WithMessage("Panchayat Name must not exceed 150 characters.");
        RuleFor(x => x.VillageName)
            .NotEmpty().WithMessage("Village Name is required.")
            .MaximumLength(150).WithMessage("Village Name must not exceed 150 characters.");
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90m, 90m).WithMessage("Latitude must be between -90 and 90.");
        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180m, 180m).WithMessage("Longitude must be between -180 and 180.");
    }
}
