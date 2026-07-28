namespace CmScheme.Common.Core.Services;

public interface INotificationService
{
    Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
    Task SendSmsAsync(string mobileNumber, string message, CancellationToken cancellationToken = default);
}
