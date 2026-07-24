using FluentValidation;

namespace CmScheme.HelpDesk.Application.Features.Ticket.CloseTicket;

public sealed class CloseTicketCommandValidator : AbstractValidator<CloseTicketCommand>
{
    public CloseTicketCommandValidator()
    {
        RuleFor(x => x.TicketId)
            .GreaterThan(0).WithMessage("TicketId must be greater than 0.");
        RuleFor(x => x.ActionBy)
            .NotEmpty().WithMessage("ActionBy is required.")
            .MaximumLength(200).WithMessage("ActionBy must not exceed 200 characters.");
        RuleFor(x => x.Remarks)
            .NotEmpty().WithMessage("Remarks is required.")
            .MaximumLength(2000).WithMessage("Remarks must not exceed 2000 characters.");
    }
}
