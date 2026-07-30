using FluentValidation;

namespace CmScheme.HelpDesk.Application.Features.Ticket.SubmitSurvey;

public sealed class SubmitSatisfactionSurveyCommandValidator : AbstractValidator<SubmitSatisfactionSurveyCommand>
{
    public SubmitSatisfactionSurveyCommandValidator()
    {
        RuleFor(x => x.TicketId)
            .GreaterThan(0).WithMessage("TicketId must be greater than 0.");
        RuleFor(x => x.UserAccountId)
            .GreaterThan(0).WithMessage("UserAccountId must be greater than 0.");
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
        RuleFor(x => x.Comments)
            .MaximumLength(500).WithMessage("Comments must not exceed 500 characters.");
    }
}
