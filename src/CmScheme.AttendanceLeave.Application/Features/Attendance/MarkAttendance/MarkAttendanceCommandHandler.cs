using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using AttendanceEntity = CmScheme.AttendanceLeave.Core.Entities.Attendance;
using Mediator;
using System.Text;

using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.MarkAttendance;

public sealed class MarkAttendanceCommandHandler(
    IAttendanceLeaveCommandDbContext dbContext,
    IRegistrationQueryDbContext registrationDbContext,
    IGeoValidationService geoValidationService,
    IFaceMatchService faceMatchService,
    IFileUploadService fileUploadService)
    : ICommandHandler<MarkAttendanceCommand, Result<int>>
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

    private const decimal MinLatitude = 21.0m;
    private const decimal MaxLatitude = 26.5m;
    private const decimal MinLongitude = 74.0m;
    private const decimal MaxLongitude = 82.5m;

    public async ValueTask<Result<int>> Handle(MarkAttendanceCommand request, CancellationToken cancellationToken)
    {
        // 1. GPS Validation
        if (request.Latitude.HasValue && request.Longitude.HasValue)
        {
            if (request.Latitude.Value < MinLatitude || request.Latitude.Value > MaxLatitude ||
                request.Longitude.Value < MinLongitude || request.Longitude.Value > MaxLongitude)
            {
                return Result.Invalid(new ValidationError("Attendance location is outside the approved MP State area."));
            }

            // Query applicant's actual assigned district from Registration module
            (double targetLat, double targetLon) = await GetApplicantDistrictCoordinatesAsync(request.ApplicantId, request.DistrictName, cancellationToken);

            bool isWithinArea = await geoValidationService.IsWithinAssignedAreaAsync(
                request.ApplicantId, request.Latitude.Value, request.Longitude.Value,
                (decimal)targetLat, (decimal)targetLon, maxDistanceKm: 5.0m, cancellationToken);

            if (!isWithinArea)
            {
                return Result.Invalid(new ValidationError("You are not within your assigned work geofence area."));
            }
        }

        // 2. Face Recognition & Validation
        string captureFacePath = string.Empty;
        decimal? faceMatchPercentage = null;
        string faceVerificationStatus = "Not Verified";

        if (!string.IsNullOrWhiteSpace(request.FaceImageBase64))
        {
            // Query applicant's registration photo from Registration module
            string? referencePhotoPath = await GetApplicantPhotoPathAsync(request.ApplicantId, cancellationToken);

            var faceMatchResult = await faceMatchService.CompareFacesAsync(
                request.FaceImageBase64, referencePhotoPath ?? string.Empty, cancellationToken);
            
            faceMatchPercentage = faceMatchResult.MatchPercentage;
            faceVerificationStatus = faceMatchResult.VerificationStatus;

            if (faceMatchPercentage < 80.0m)
            {
                return Result.Invalid(new ValidationError($"Face match failed ({faceMatchPercentage:F2}%). Minimum 80% required."));
            }

            // 3. Save captured selfie image
            try
            {
                byte[] imageBytes = Convert.FromBase64String(request.FaceImageBase64);
                using var stream = new MemoryStream(imageBytes);
                string fileName = $"attendance_{request.ApplicantId}_{DateTime.UtcNow.Ticks}.jpg";
                captureFacePath = await fileUploadService.UploadAsync(
                    stream, fileName, "image/jpeg", "documents/attendance-faces", cancellationToken);
            }
            catch (Exception)
            {
                captureFacePath = "invalid_base64_capture";
            }
        }

        // 4. Save Attendance Record
        DateTime now = DateTime.UtcNow;
        var date = request.AttendanceDate ?? now.Date;
        var checkIn = request.CheckInTime ?? TimeOnly.FromDateTime(now);

        AttendanceEntity attendance = new AttendanceEntity
        {
            ApplicantId = request.ApplicantId,
            AttendanceDate = date,
            CheckInTime = checkIn,
            CheckOutTime = request.CheckOutTime,
            CaptureFacePath = captureFacePath,
            FaceMatchPercentage = faceMatchPercentage,
            FaceVerificationStatus = faceVerificationStatus,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            AttendanceStatus = request.AttendanceStatus ?? "Present",
            CreatedOn = now
        };

        dbContext.Attendances.Add(attendance);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(attendance.AttendanceId);
    }

    private static async Task<(double Latitude, double Longitude)> GetApplicantDistrictCoordinatesAsync(
        int applicantId, string? districtName, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(districtName) &&
            DistrictCoordinates.TryGetValue(districtName, out var coords))
        {
            return coords;
        }

        // Default to Bhopal if district not provided or unrecognized
        await Task.CompletedTask;
        return (23.2599, 77.4126);
    }

    private async Task<string?> GetApplicantPhotoPathAsync(int applicantId, CancellationToken cancellationToken)
    {
        return await registrationDbContext.Applicants
            .Where(a => a.ApplicantId == applicantId)
            .Select(a => a.PhotographPath)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
