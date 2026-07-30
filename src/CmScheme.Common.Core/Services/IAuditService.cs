namespace CmScheme.Common.Core.Services;

public interface IAuditService
{
    Task LogAsync(string action, string entityName, string? entityId = null, string? oldValues = null, string? newValues = null, CancellationToken ct = default);
}
