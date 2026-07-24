namespace CmScheme.AttendanceLeave.Core.Dtos;

public sealed class AttendanceDto
{
    public int AttendanceId { get; init; }
    public int ApplicantId { get; init; }
    public DateTime AttendanceDate { get; init; }
    public TimeOnly CheckInTime { get; init; }
    public TimeOnly? CheckOutTime { get; init; }
    public string CaptureFacePath { get; init; } = null!;
    public decimal? FaceMatchPercentage { get; init; }
    public string FaceVerificationStatus { get; init; } = null!;
    public decimal? Latitude { get; init; }
    public decimal? Longitude { get; init; }
    public string AttendanceStatus { get; init; } = null!;
    public DateTime CreatedOn { get; init; }
}

public sealed class LeaveApplicationDto
{
    public int LeaveApplicationId { get; init; }
    public int ApplicantId { get; init; }
    public string LeaveType { get; init; } = null!;
    public DateTime FromDate { get; init; }
    public DateTime ToDate { get; init; }
    public int NumberOfDays { get; init; }
    public string HalfDayFullDay { get; init; } = null!;
    public string LeaveReason { get; init; } = null!;
    public string? AttachmentPath { get; init; }
    public string ReportingManagerName { get; init; } = null!;
    public string Status { get; init; } = null!;
    public DateTime CreatedOn { get; init; }
    public string CreatedBy { get; init; } = null!;
    public DateTime? ModifiedOn { get; init; }
}

public sealed class LeaveBalanceDto
{
    public int LeaveBalanceId { get; init; }
    public int ApplicantId { get; init; }
    public string LeaveType { get; init; } = null!;
    public int OpeningBalance { get; init; }
    public int AvailedLeave { get; init; }
    public int PendingApprovalLeave { get; init; }
    public int AvailableBalance { get; init; }
}

public sealed class LeaveStatusDto
{
    public int LeaveApplicationId { get; init; }
    public string LeaveType { get; init; } = null!;
    public DateTime FromDate { get; init; }
    public DateTime ToDate { get; init; }
    public int NumberOfDays { get; init; }
    public string Status { get; init; } = null!;
    public DateTime CreatedOn { get; init; }
}
