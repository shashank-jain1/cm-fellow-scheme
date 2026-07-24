using FluentValidation;

namespace CmScheme.WorkAllocation.Application.Features.SurveyRecord.CreateSurveyRecord;

public sealed class CreateSurveyRecordCommandValidator : AbstractValidator<CreateSurveyRecordCommand>
{
    public CreateSurveyRecordCommandValidator()
    {
        RuleFor(x => x.TaskProgressId)
            .GreaterThan(0).WithMessage("TaskProgressId must be greater than 0.");
        RuleFor(x => x.InternName)
            .NotEmpty().WithMessage("InternName is required.")
            .MaximumLength(200).WithMessage("InternName must not exceed 200 characters.");
        RuleFor(x => x.SurveyPersonName)
            .NotEmpty().WithMessage("SurveyPersonName is required.")
            .MaximumLength(200).WithMessage("SurveyPersonName must not exceed 200 characters.");
        RuleFor(x => x.MobileNumber)
            .NotEmpty().WithMessage("MobileNumber is required.")
            .MaximumLength(20).WithMessage("MobileNumber must not exceed 20 characters.");
        RuleFor(x => x.PanchayatName)
            .NotEmpty().WithMessage("PanchayatName is required.")
            .MaximumLength(200).WithMessage("PanchayatName must not exceed 200 characters.");
        RuleFor(x => x.VillageName)
            .NotEmpty().WithMessage("VillageName is required.")
            .MaximumLength(200).WithMessage("VillageName must not exceed 200 characters.");
        RuleFor(x => x.SurveyStatus)
            .NotEmpty().WithMessage("SurveyStatus is required.")
            .MaximumLength(50).WithMessage("SurveyStatus must not exceed 50 characters.");
    }
}
