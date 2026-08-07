using System.Net;
using System.Net.Mail;
using System.Net.Http.Json;
using System.Text.Json;
using CmScheme.Common.Core.Entities;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class SmtpNotificationService(
    IConfiguration configuration,
    ILogger<SmtpNotificationService> logger,
    IRegistrationCommandDbContext registrationDbContext,
    IHttpClientFactory httpClientFactory) : INotificationService
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

    public async Task SendSmsAsync(string mobileNumber, string message, CancellationToken ct = default)
    {
        string? apiKey = configuration["Sms:ApiKey"];
        string? senderId = configuration["Sms:SenderId"];
        string? route = configuration["Sms:Route"] ?? "Transactional";

        if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(senderId))
        {
            logger.LogWarning("SMS configuration (Sms:ApiKey, Sms:SenderIds) is missing. Skipping SMS to {Mobile}.", mobileNumber);
            return;
        }

        try
        {
            HttpClient client = httpClientFactory.CreateClient();
            string url = $"https://api.msg91.com/api/v5/flow?apiKey={apiKey}";

            var payload = new
            {
                flow_id = configuration["Sms:FlowId"],
                sender = senderId,
                mobiles = $"91{mobileNumber}",
                var1 = message
            };

            HttpResponseMessage response = await client.PostAsJsonAsync(url, payload, ct);
            string responseBody = await response.Content.ReadAsStringAsync(ct);

            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation("SMS sent to {Mobile} via MSG91", mobileNumber);

                Notification notification = new Notification
                {
                    Channel = "SMS",
                    Recipient = mobileNumber,
                    Subject = "SMS",
                    Body = message,
                    Status = "Sent",
                    SentOn = DateTime.UtcNow
                };

                registrationDbContext.Notifications.Add(notification);
                await registrationDbContext.SaveChangesAsync(ct);
            }
            else
            {
                logger.LogError("SMS failed to {Mobile}: {Status} - {Body}", mobileNumber, response.StatusCode, responseBody);

                Notification notification = new Notification
                {
                    Channel = "SMS",
                    Recipient = mobileNumber,
                    Subject = "SMS",
                    Body = message,
                    Status = "Failed",
                    ErrorMessage = $"HTTP {response.StatusCode}: {responseBody}",
                    SentOn = DateTime.UtcNow
                };

                registrationDbContext.Notifications.Add(notification);
                await registrationDbContext.SaveChangesAsync(ct);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send SMS to {Mobile}", mobileNumber);

            Notification notification = new Notification
            {
                Channel = "SMS",
                Recipient = mobileNumber,
                Subject = "SMS",
                Body = message,
                Status = "Failed",
                ErrorMessage = ex.Message,
                SentOn = DateTime.UtcNow
            };

            registrationDbContext.Notifications.Add(notification);
            await registrationDbContext.SaveChangesAsync(ct);
        }
    }
}
