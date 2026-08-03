using CmScheme.Common.Core.Entities;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using Microsoft.Extensions.Logging;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class ConsoleSmsService(
    ILogger<ConsoleSmsService> logger,
    IRegistrationCommandDbContext registrationDbContext) : ISmsService
{
    public async Task SendSmsAsync(string phoneNumber, string message, CancellationToken ct = default)
    {
        try
        {
            logger.LogInformation("SMS to {PhoneNumber}: {Message}", phoneNumber, message);
            Console.WriteLine($"[SMS] To: {phoneNumber} | Message: {message}");

            Notification notification = new Notification
            {
                Channel = "Sms",
                Recipient = phoneNumber,
                Subject = "",
                Body = message,
                Status = "Sent",
                SentOn = DateTime.UtcNow
            };

            registrationDbContext.Notifications.Add(notification);
            await registrationDbContext.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send SMS to {PhoneNumber}", phoneNumber);

            Notification notification = new Notification
            {
                Channel = "Sms",
                Recipient = phoneNumber,
                Subject = "",
                Body = message,
                Status = "Failed",
                ErrorMessage = ex.Message,
                SentOn = DateTime.UtcNow
            };

            registrationDbContext.Notifications.Add(notification);
            await registrationDbContext.SaveChangesAsync(ct);

            throw;
        }
    }
}
