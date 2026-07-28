using FluentValidation;
using CmScheme.Common.Core;

namespace CmScheme.Training.Application.Features.Training.UpdateTrainingStatus;

public sealed class UpdateTrainingStatusCommandValidator : AbstractValidator<UpdateTrainingStatusCommand>
{
    public UpdateTrainingStatusCommandValidator()
    {
        RuleFor(x => x.TrainingScheduleId)
            .GreaterThan(0).WithMessage("Valid training schedule ID is required.");

        RuleFor(x => x.NewStatus)
            .NotEmpty().WithMessage("New status is required.")
            .Must(s => new[]
            {
                Statuses.Training.Scheduled,
                Statuses.Training.Ongoing,
                Statuses.Training.Completed,
                Statuses.Training.Closed,
                Statuses.Training.Cancelled
            }.Contains(s, StringComparer.OrdinalIgnoreCase))
            .WithMessage("Invalid training status.");
    }
}
