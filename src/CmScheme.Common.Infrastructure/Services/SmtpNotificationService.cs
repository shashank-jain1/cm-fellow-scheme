using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CmScheme.Common.Core.Services;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class SmtpNotificationService(
    IConfiguration configuration,
    ILogger<SmtpNotificationService> logger) : INotificationService
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

        await client.SendMailAsync(message, ct);

        logger.LogInformation("Email sent to {To} with subject {Subject}", to, subject);
    }

    public Task SendSmsAsync(string mobileNumber, string message, CancellationToken ct = default)
    {
        logger.LogInformation("SMS notification to {Mobile}: {Message}", mobileNumber, message);
        return Task.CompletedTask;
    }
}
