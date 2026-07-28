using FluentValidation;

namespace CmScheme.Certificate.Application.Features.Exit.CloseAndArchiveRecord;

public sealed class CloseAndArchiveRecordCommandValidator : AbstractValidator<CloseAndArchiveRecordCommand>
{
    public CloseAndArchiveRecordCommandValidator()
    {
        RuleFor(x => x.ExitRecordId)
            .GreaterThan(0).WithMessage("Exit Record ID is required.");
        RuleFor(x => x.ApprovedBy)
            .GreaterThan(0).WithMessage("Approved By is required.");
    }
}
