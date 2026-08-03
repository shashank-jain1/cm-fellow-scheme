using System.Net;
using System.Net.Mail;
using CmScheme.Common.Core.Entities;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class SmtpNotificationService(
    IConfiguration configuration,
    ILogger<SmtpNotificationService> logger,
    IRegistrationCommandDbContext registrationDbContext) : INotificationService
{
    public async Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default)
    {
        string host = configuration["Smtp:Host"] ?? "smtp.gmail.com";
        int port = int.TryParse(configuration["Smtp:Port"], out int p) ? p : 587;
        string username = configuration["Smtp:Username"] ?? "";
        string password = configuration["Smtp:Password"] ?? "";
        string fromAddress = configuration["Smtp:FromAddress"] ?? "noreply@cmportal.gov.in";
        bool enableSsl = bool.TryParse(configuration["Smtp:EnableSsl"], out bool ssl) && ssl;

        using SmtpClient client = new(host, port)
        {
            Credentials = new NetworkCredential(username, password),
            EnableSsl = enableSsl,
        };

        using MailMessage message = new(fromAddress, to, subject, body)
        {
            IsBodyHtml = true,
        };

        try
        {
            await client.SendMailAsync(message, ct);

            logger.LogInformation("Email sent to {To} with subject {Subject}", to, subject);

            Notification notification = new Notification
            {
                Channel = "Email",
                Recipient = to,
                Subject = subject,
                Body = body,
                Status = "Sent",
                SentOn = DateTime.UtcNow
            };

            registrationDbContext.Notifications.Add(notification);
            await registrationDbContext.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send email to {To} with subject {Subject}", to, subject);

            Notification notification = new Notification
            {
                Channel = "Email",
                Recipient = to,
                Subject = subject,
                Body = body,
                Status = "Failed",
                ErrorMessage = ex.Message,
                SentOn = DateTime.UtcNow
            };

            registrationDbContext.Notifications.Add(notification);
            await registrationDbContext.SaveChangesAsync(ct);

            throw;
        }
    }

    public Task SendSmsAsync(string mobileNumber, string message, CancellationToken ct = default)
    {
        logger.LogInformation("SMS notification to {Mobile}: {Message}", mobileNumber, message);
        return Task.CompletedTask;
    }
}
