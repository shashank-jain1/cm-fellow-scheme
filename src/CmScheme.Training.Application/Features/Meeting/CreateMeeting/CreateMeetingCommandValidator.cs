using FluentValidation;

namespace CmScheme.Training.Application.Features.Meeting.CreateMeeting;

public sealed class CreateMeetingCommandValidator
    : AbstractValidator<CreateMeetingCommand>
{
    public CreateMeetingCommandValidator()
    {
        RuleFor(x => x.MeetingTitle)
            .NotEmpty().WithMessage("Meeting title is required.")
            .MaximumLength(250).WithMessage("Meeting title must not exceed 250 characters.");
        RuleFor(x => x.Date).NotEmpty().WithMessage("Meeting date is required.");
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).WithMessage("End time must be after start time.");
    }
}
