using FluentValidation;

namespace CmScheme.Training.Application.Features.Training.CreateTraining;

internal sealed class CreateTrainingCommandValidator
    : AbstractValidator<CreateTrainingCommand>
{
    public CreateTrainingCommandValidator()
    {
        RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("Valid project is required.");
        RuleFor(x => x.TrainingTitle).NotEmpty().MaximumLength(250).WithMessage("Training title is required.");
        RuleFor(x => x.Date).NotEmpty().WithMessage("Training date is required.");
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).WithMessage("End time must be after start time.");
    }
}
