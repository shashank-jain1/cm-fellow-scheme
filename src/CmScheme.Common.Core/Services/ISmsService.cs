namespace CmScheme.Common.Core.Services;

public interface ISmsService
{
    Task SendSmsAsync(string phoneNumber, string message, CancellationToken ct = default);
}
