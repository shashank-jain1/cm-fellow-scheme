using FluentValidation;

namespace CmScheme.Registration.Application.Features.BulkImport.ImportUsers;

public sealed class ImportUsersCommandValidator : AbstractValidator<ImportUsersCommand>
{
    public ImportUsersCommandValidator()
    {
        RuleFor(x => x.CsvStream)
            .NotNull()
            .WithMessage("CSV file stream is required.");
    }
}
