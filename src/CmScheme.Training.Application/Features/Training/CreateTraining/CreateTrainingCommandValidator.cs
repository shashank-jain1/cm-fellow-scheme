using FluentValidation;

namespace CmScheme.Training.Application.Features.Training.CreateTraining;

public sealed class CreateTrainingCommandValidator
    : AbstractValidator<CreateTrainingCommand>
{
    public CreateTrainingCommandValidator()
    {
        RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("Valid project is required.");
        RuleFor(x => x.TrainingTitle)
            .NotEmpty().WithMessage("Training title is required.")
            .MaximumLength(250).WithMessage("Training title must not exceed 250 characters.");
        RuleFor(x => x.Date).NotEmpty().WithMessage("Training date is required.");
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).WithMessage("End time must be after start time.");
    }
}
