namespace CmScheme.Common.Core.Services;

public interface IDatabaseBackupService
{
    Task<string> BackupAsync(CancellationToken ct = default);
}
