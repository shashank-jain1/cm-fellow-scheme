using FluentValidation;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.MarkAttendance;

public sealed class MarkAttendanceCommandValidator : AbstractValidator<MarkAttendanceCommand>
{
    private static readonly Dictionary<string, (double Latitude, double Longitude)> DistrictCoordinates = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Bhopal"] = (23.2599, 77.4126),
        ["Indore"] = (22.7196, 75.8577),
        ["Jabalpur"] = (23.1815, 79.9864),
        ["Gwalior"] = (26.2183, 78.1828),
        ["Ujjain"] = (23.1793, 75.7849),
        ["Sagar"] = (23.8388, 78.7378),
        ["Satna"] = (24.5807, 80.8324),
        ["Rewa"] = (24.5360, 81.3000),
        ["Vidisha"] = (23.5241, 77.8071),
        ["Sehore"] = (23.1976, 77.0819),
    };

    private static readonly (double Latitude, double Longitude) DefaultCenter = (23.2599, 77.4126);

    public MarkAttendanceCommandValidator()
    {
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("ApplicantId must be greater than 0.");
        RuleFor(x => x.AttendanceDate)
            .NotEmpty().WithMessage("AttendanceDate is required.");
        RuleFor(x => x.CaptureFacePath)
            .NotEmpty().WithMessage("CaptureFacePath is required.")
            .MaximumLength(500).WithMessage("CaptureFacePath must not exceed 500 characters.");
        RuleFor(x => x.FaceMatchPercentage)
            .NotNull().WithMessage("FaceMatchPercentage is required.")
            .GreaterThanOrEqualTo(80m).WithMessage("Face match must be at least 80% for attendance verification.");
        RuleFor(x => x.FaceVerificationStatus)
            .NotEmpty().WithMessage("FaceVerificationStatus is required.")
            .MaximumLength(50).WithMessage("FaceVerificationStatus must not exceed 50 characters.");
        RuleFor(x => x.AttendanceStatus)
            .NotEmpty().WithMessage("AttendanceStatus is required.")
            .MaximumLength(50).WithMessage("AttendanceStatus must not exceed 50 characters.");
        RuleFor(x => x.Latitude)
            .InclusiveBetween(21.0m, 26.5m)
            .When(x => x.Latitude.HasValue)
            .WithMessage("Latitude must be within Madhya Pradesh bounds (21.0 - 26.5).");
        RuleFor(x => x.Longitude)
            .InclusiveBetween(74.0m, 82.5m)
            .When(x => x.Longitude.HasValue)
            .WithMessage("Longitude must be within Madhya Pradesh bounds (74.0 - 82.5).");
        RuleFor(x => x)
            .Must(x =>
            {
                if (!x.Latitude.HasValue || !x.Longitude.HasValue)
                {
                    return true;
                }

                (double centerLat, double centerLon) = DefaultCenter;

                if (!string.IsNullOrWhiteSpace(x.DistrictName) &&
                    DistrictCoordinates.TryGetValue(x.DistrictName.Trim(), out var coords))
                {
                    centerLat = coords.Latitude;
                    centerLon = coords.Longitude;
                }

                double distance = CalculateDistance(
                    (double)x.Latitude.Value,
                    (double)x.Longitude.Value,
                    centerLat,
                    centerLon);
                return distance <= 5000;
            })
            .When(x => x.Latitude.HasValue && x.Longitude.HasValue)
            .WithMessage("Attendance rejected: GPS coordinates are more than 5km from assigned block/district center.");
    }

    private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double EarthRadiusMeters = 6371000.0;
        const double DegreesToRadians = Math.PI / 180.0;

        double dLat = (lat2 - lat1) * DegreesToRadians;
        double dLon = (lon2 - lon1) * DegreesToRadians;

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(lat1 * DegreesToRadians) * Math.Cos(lat2 * DegreesToRadians) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

        return EarthRadiusMeters * c;
    }
}
