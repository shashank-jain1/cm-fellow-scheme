using FluentValidation;

namespace CmScheme.HelpDesk.Application.Features.Ticket.ResolveTicket;

public sealed class ResolveTicketCommandValidator : AbstractValidator<ResolveTicketCommand>
{
    public ResolveTicketCommandValidator()
    {
        RuleFor(x => x.TicketId)
            .GreaterThan(0).WithMessage("TicketId must be greater than 0.");
        RuleFor(x => x.ActionBy)
            .NotEmpty().WithMessage("ActionBy is required.")
            .MaximumLength(200).WithMessage("ActionBy must not exceed 200 characters.");
        RuleFor(x => x.ResolutionRemarks)
            .NotEmpty().WithMessage("ResolutionRemarks is required.")
            .MaximumLength(2000).WithMessage("ResolutionRemarks must not exceed 2000 characters.");
    }
}
