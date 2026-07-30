namespace CmScheme.Common.Core.Services;

public interface IBulkImportService
{
    Task<int> ImportUsersFromCsvAsync(Stream csvStream, CancellationToken ct = default);
}
