using Microsoft.Extensions.Logging;

namespace CmScheme.Common.Core.Services;

public sealed class StubNotificationService(ILogger<StubNotificationService> logger) : INotificationService
{
    public Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Email notification sent to {To}: {Subject}", to, subject);
        return Task.CompletedTask;
    }

    public Task SendSmsAsync(string mobileNumber, string message, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("SMS notification sent to {Mobile}: {Message}", mobileNumber, message);
        return Task.CompletedTask;
    }
}
