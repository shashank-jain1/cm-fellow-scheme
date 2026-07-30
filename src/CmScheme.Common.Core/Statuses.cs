namespace CmScheme.Common.Core;

public static class Statuses
{
    public static class Registration
    {
        public const string Pending = "Pending";
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";
    }

    public static class Ticket
    {
        public const string Open = "Open";
        public const string InProgress = "In Progress";
        public const string Resolved = "Resolved";
        public const string Closed = "Closed";
        public const string Rejected = "Rejected";
        public const string Escalated = "Escalated";
    }

    public static class TicketAction
    {
        public const string Created = "Created";
        public const string Resolved = "Resolved";
        public const string Closed = "Closed";
        public const string Escalated = "Escalated";
    }

    public static class Leave
    {
        public const string Pending = "Pending";
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";
        public const string Cancelled = "Cancelled";
    }

    public static class Certificate
    {
        public const string Applied = "Applied";
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";
        public const string Generated = "Generated";
        public const string Issued = "Issued";
    }

    public static class Training
    {
        public const string Scheduled = "Scheduled";
        public const string Ongoing = "Ongoing";
        public const string Completed = "Completed";
        public const string Closed = "Closed";
        public const string Cancelled = "Cancelled";
        public const string TrainingType = "Training";
        public const string Meeting = "Meeting";
    }

    public static class Survey
    {
        public const string Submitted = "Submitted";
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";
        public const string Completed = "Completed";
    }

    public static class WorkStatus
    {
        public const string Assigned = "Assigned";
        public const string InProgress = "In Progress";
        public const string Completed = "Completed";
        public const string Hold = "Hold";
    }

    public static class PerformanceGrade
    {
        public const string APlus = "A+";
        public const string A = "A";
        public const string BPlus = "B+";
        public const string B = "B";
        public const string C = "C";
        public const string D = "D";
    }

    public static class PerformanceStatus
    {
        public const string Excellent = "Excellent";
        public const string Good = "Good";
        public const string Average = "Average";
        public const string Poor = "Poor";
    }

    public static class ReviewLevel
    {
        public const string Fellow = "Fellow";
        public const string Coordinator = "Coordinator";
        public const string Admin = "Admin";
    }

    public static class ReviewStatus
    {
        public const string Draft = "Draft";
        public const string Submitted = "Submitted";
        public const string UnderReview = "Under Review";
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";
    }

    public static class Attendance
    {
        public const string Present = "Present";
        public const string Absent = "Absent";
        public const string Verified = "Verified";
        public const string NotVerified = "Not Verified";
        public const string CheckedOut = "CheckedOut";
    }

    public static class Priority
    {
        public const string High = "High";
        public const string Medium = "Medium";
        public const string Low = "Low";
        public const string Critical = "Critical";
    }
}
