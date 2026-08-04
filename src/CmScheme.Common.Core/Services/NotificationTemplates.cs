namespace CmScheme.Common.Core.Services;

public static class NotificationTemplates
{
    public static (string Subject, string Body, string SmsMessage) RegistrationApproved(string name)
    {
        string subject = "Registration Approved - CM Fellow Program";
        string body = $"Dear {name},\n\n" +
                      "Your registration for the CM Fellow Program has been approved.\n" +
                      "You can now log in to the portal using your registered credentials.\n\n" +
                      "Best regards,\n" +
                      "CM Fellow Program Team";
        string sms = $"Dear {name}, your CM Fellow registration is approved. Please log in to access your portal.";
        return (subject, body, sms);
    }

    public static (string Subject, string Body, string SmsMessage) RegistrationRejected(string name, string reason)
    {
        string subject = "Registration Application Status Update - CM Fellow Program";
        string body = $"Dear {name},\n\n" +
                      "We regret to inform you that your registration application for the CM Fellow Program was not approved.\n" +
                      $"Reason: {reason}\n\n" +
                      "Best regards,\n" +
                      "CM Fellow Program Team";
        string sms = $"Dear {name}, your CM Fellow registration application was rejected. Reason: {reason}";
        return (subject, body, sms);
    }

    public static (string Subject, string Body, string SmsMessage) LeaveApplied(string applicantName, string leaveType, decimal days, string dates)
    {
        string subject = $"New Leave Application: {applicantName} ({leaveType})";
        string body = $"A new leave application has been submitted.\n\n" +
                      $"Applicant: {applicantName}\n" +
                      $"Leave Type: {leaveType}\n" +
                      $"Duration: {days} day(s) ({dates})\n\n" +
                      "Please log in to review and approve or reject the request.";
        string sms = $"New leave request submitted by {applicantName} for {days} day(s) ({leaveType}).";
        return (subject, body, sms);
    }

    public static (string Subject, string Body, string SmsMessage) LeaveApproved(string name, string leaveType, decimal days)
    {
        string subject = "Leave Application Approved";
        string body = $"Dear {name},\n\n" +
                      $"Your leave application ({leaveType}) for {days} day(s) has been approved.\n\n" +
                      "Best regards,\n" +
                      "CM Fellow Program Team";
        string sms = $"Dear {name}, your leave application for {days} day(s) ({leaveType}) has been approved.";
        return (subject, body, sms);
    }

    public static (string Subject, string Body, string SmsMessage) LeaveRejected(string name, string leaveType, decimal days, string reason)
    {
        string subject = "Leave Application Rejected";
        string body = $"Dear {name},\n\n" +
                      $"Your leave application ({leaveType}) for {days} day(s) was rejected.\n" +
                      $"Remarks: {reason}\n\n" +
                      "Best regards,\n" +
                      "CM Fellow Program Team";
        string sms = $"Dear {name}, your leave application for {days} day(s) ({leaveType}) was rejected.";
        return (subject, body, sms);
    }

    public static (string Subject, string Body, string SmsMessage) CertificateApproved(string name, string certificateType)
    {
        string subject = $"{certificateType} Request Approved";
        string body = $"Dear {name},\n\n" +
                      $"Your request for a {certificateType} has been approved.\n" +
                      "You can now view and download your certificate from the portal.\n\n" +
                      "Best regards,\n" +
                      "CM Fellow Program Team";
        string sms = $"Dear {name}, your {certificateType} request has been approved and is available for download.";
        return (subject, body, sms);
    }

    public static (string Subject, string Body, string SmsMessage) TicketEscalated(int ticketId, string category)
    {
        string subject = $"Ticket #{ticketId} Escalated ({category})";
        string body = $"Support Ticket #{ticketId} in category '{category}' has been escalated.\n" +
                      "Immediate attention is required.\n\n" +
                      "Please log in to review ticket details.";
        string sms = $"Ticket #{ticketId} ({category}) has been escalated and requires your attention.";
        return (subject, body, sms);
    }

    public static (string Subject, string Body, string SmsMessage) TicketResolved(int ticketId, string remarks)
    {
        string subject = $"Ticket #{ticketId} Resolved";
        string body = $"Your support ticket #{ticketId} has been resolved.\n\n" +
                      $"Resolution Remarks: {remarks}\n\n" +
                      "Please log in to submit satisfaction feedback.\n\n" +
                      "Best regards,\n" +
                      "Support Team";
        string sms = $"Ticket #{ticketId} has been resolved. Please log in to view details and submit feedback.";
        return (subject, body, sms);
    }

    public static (string Subject, string Body, string SmsMessage) TicketClosed(int ticketId, string remarks)
    {
        string subject = $"Ticket #{ticketId} Closed";
        string body = $"Support ticket #{ticketId} has been closed.\n\n" +
                      $"Remarks: {remarks}\n\n" +
                      "Thank you for contacting support.";
        string sms = $"Ticket #{ticketId} has been closed. Thank you.";
        return (subject, body, sms);
    }

    public static (string Subject, string Body, string SmsMessage) TaskVerified(string taskName, string status, string remarks)
    {
        string subject = $"Task Verification Update: {taskName}";
        string body = $"Your submitted task '{taskName}' has been reviewed.\n\n" +
                      $"Status: {status}\n" +
                      $"Remarks: {remarks}\n\n" +
                      "Best regards,\n" +
                      "CM Fellow Program Team";
        string sms = $"Task '{taskName}' verification updated to status '{status}'.";
        return (subject, body, sms);
    }
}
