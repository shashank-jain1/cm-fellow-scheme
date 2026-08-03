namespace CmScheme.Common.Core.Entities;

public class Notification
{
    public int NotificationId { get; set; }
    public int? UserId { get; set; }
    public string Channel { get; set; } = null!;
    public string Recipient { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public string Status { get; set; } = "Sent";
    public string? ErrorMessage { get; set; }
    public DateTime SentOn { get; set; } = DateTime.UtcNow;
}
