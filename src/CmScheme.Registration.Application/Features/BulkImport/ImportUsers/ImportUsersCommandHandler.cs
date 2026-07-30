using Ardalis.Result;
using Mediator;
using CmScheme.Common.Core.Services;

namespace CmScheme.Registration.Application.Features.BulkImport.ImportUsers;

public sealed class ImportUsersCommandHandler(IBulkImportService bulkImportService)
    : ICommandHandler<ImportUsersCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        ImportUsersCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            int count = await bulkImportService.ImportUsersFromCsvAsync(
                request.CsvStream, cancellationToken);
            return Result<int>.Success(count);
        }
        catch (FormatException ex)
        {
            return Result<int>.Invalid(new ValidationError("CsvFile", ex.Message));
        }
    }
}
