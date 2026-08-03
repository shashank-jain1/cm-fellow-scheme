using Microsoft.Extensions.Logging;
using CmScheme.Common.Core.Services;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class ConsoleSmsService(ILogger<ConsoleSmsService> logger) : ISmsService
{
    public Task SendSmsAsync(string phoneNumber, string message, CancellationToken ct = default)
    {
        logger.LogInformation("SMS to {PhoneNumber}: {Message}", phoneNumber, message);
        Console.WriteLine($"[SMS] To: {phoneNumber} | Message: {message}");
        return Task.CompletedTask;
    }
}
