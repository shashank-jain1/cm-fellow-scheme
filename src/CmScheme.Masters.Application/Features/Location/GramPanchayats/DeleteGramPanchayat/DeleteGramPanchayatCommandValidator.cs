using FluentValidation;

namespace CmScheme.Masters.Application.Features.Location.GramPanchayats.DeleteGramPanchayat;

public sealed class DeleteGramPanchayatCommandValidator : AbstractValidator<DeleteGramPanchayatCommand>
{
    public DeleteGramPanchayatCommandValidator()
    {
        RuleFor(x => x.GramPanchayatId)
            .GreaterThan(0).WithMessage("Valid gram panchayat is required.");
    }
}
