using FluentValidation;

namespace CmScheme.Training.Application.Features.Meeting.UpdateMeeting;

public sealed class UpdateMeetingCommandValidator : AbstractValidator<UpdateMeetingCommand>
{
    public UpdateMeetingCommandValidator()
    {
        RuleFor(x => x.TrainingScheduleId)
            .GreaterThan(0).WithMessage("Meeting ID is required.");
        RuleFor(x => x.MeetingTitle)
            .NotEmpty().WithMessage("Meeting Title is required.")
            .MaximumLength(250).WithMessage("Meeting Title must not exceed 250 characters.");
        RuleFor(x => x.MeetingAgenda)
            .NotEmpty().WithMessage("Meeting Agenda is required.")
            .MaximumLength(1000).WithMessage("Meeting Agenda must not exceed 1000 characters.");
        RuleFor(x => x.MeetingDescription)
            .NotEmpty().WithMessage("Meeting Description is required.")
            .MaximumLength(2000).WithMessage("Meeting Description must not exceed 2000 characters.");
        RuleFor(x => x.ConductPersonId)
            .GreaterThan(0).WithMessage("Conduct Person is required.");
        RuleFor(x => x.CoordinatorId)
            .GreaterThan(0).WithMessage("Coordinator is required.");
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.");
        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Start Time is required.");
        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("End Time is required.")
            .GreaterThan(x => x.StartTime).WithMessage("End Time must be greater than Start Time.");
        RuleFor(x => x.Remarks)
            .MaximumLength(2000).WithMessage("Remarks must not exceed 2000 characters.");
    }
}
