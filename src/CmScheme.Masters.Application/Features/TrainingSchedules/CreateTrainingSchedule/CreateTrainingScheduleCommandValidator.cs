using FluentValidation;

namespace CmScheme.Masters.Application.Features.TrainingSchedules.CreateTrainingSchedule;

public sealed class CreateTrainingScheduleCommandValidator : AbstractValidator<CreateTrainingScheduleCommand>
{
    public CreateTrainingScheduleCommandValidator()
    {
        RuleFor(x => x.CalendarYear)
            .NotEmpty().WithMessage("Calendar Year is required.")
            .MaximumLength(20).WithMessage("Calendar Year must not exceed 20 characters.");
        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("Project is required.");
        RuleFor(x => x.TrainingDate)
            .NotEmpty().WithMessage("Training Date is required.");
        RuleFor(x => x.VenueName)
            .MaximumLength(250).WithMessage("Venue Name must not exceed 250 characters.");
        RuleFor(x => x.TrainingDescription)
            .MaximumLength(1000).WithMessage("Training Description must not exceed 1000 characters.");
    }
}
