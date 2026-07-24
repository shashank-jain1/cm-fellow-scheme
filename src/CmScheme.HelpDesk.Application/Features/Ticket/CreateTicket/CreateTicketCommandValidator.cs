using FluentValidation;

namespace CmScheme.HelpDesk.Application.Features.Ticket.CreateTicket;

public sealed class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
{
    public CreateTicketCommandValidator()
    {
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("ApplicantId must be greater than 0.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be valid.")
            .MaximumLength(200).WithMessage("Email must not exceed 200 characters.");
        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("Mobile is required.")
            .MaximumLength(20).WithMessage("Mobile must not exceed 20 characters.");
        RuleFor(x => x.IssueCategory)
            .NotEmpty().WithMessage("IssueCategory is required.")
            .MaximumLength(100).WithMessage("IssueCategory must not exceed 100 characters.");
        RuleFor(x => x.IssueDescription)
            .NotEmpty().WithMessage("IssueDescription is required.")
            .MaximumLength(2000).WithMessage("IssueDescription must not exceed 2000 characters.");
        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required.")
            .MaximumLength(20).WithMessage("Priority must not exceed 20 characters.");
    }
}
