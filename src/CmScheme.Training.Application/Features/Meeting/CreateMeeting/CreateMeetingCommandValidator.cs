using FluentValidation;

namespace CmScheme.Training.Application.Features.Meeting.CreateMeeting;

internal sealed class CreateMeetingCommandValidator
    : AbstractValidator<CreateMeetingCommand>
{
    public CreateMeetingCommandValidator()
    {
        RuleFor(x => x.MeetingTitle).NotEmpty().MaximumLength(250).WithMessage("Meeting title is required.");
        RuleFor(x => x.Date).NotEmpty().WithMessage("Meeting date is required.");
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).WithMessage("End time must be after start time.");
    }
}
