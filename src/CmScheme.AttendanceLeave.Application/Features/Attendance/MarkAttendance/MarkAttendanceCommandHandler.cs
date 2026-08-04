using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using AttendanceEntity = CmScheme.AttendanceLeave.Core.Entities.Attendance;
using Mediator;

using CmScheme.Common.Core.Services;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.MarkAttendance;

public sealed class MarkAttendanceCommandHandler(
    IAttendanceLeaveCommandDbContext dbContext,
    IGeoValidationService geoValidationService)
    : ICommandHandler<MarkAttendanceCommand, Result<int>>
{
    private const decimal MinLatitude = 21.0m;
    private const decimal MaxLatitude = 26.5m;
    private const decimal MinLongitude = 74.0m;
    private const decimal MaxLongitude = 82.5m;

    public async ValueTask<Result<int>> Handle(MarkAttendanceCommand request, CancellationToken cancellationToken)
    {
        if (request.Latitude.HasValue && request.Longitude.HasValue)
        {
            if (request.Latitude.Value < MinLatitude || request.Latitude.Value > MaxLatitude ||
                request.Longitude.Value < MinLongitude || request.Longitude.Value > MaxLongitude)
            {
                return Result.Invalid(new ValidationError("Attendance location is outside the approved area"));
            }

            bool isWithinArea = await geoValidationService.IsWithinAssignedAreaAsync(
                request.ApplicantId, request.Latitude.Value, request.Longitude.Value, maxDistanceKm: 5.0m, cancellationToken);

            if (!isWithinArea)
            {
                return Result.Invalid(new ValidationError("You are not within your assigned work geofence area."));
            }
        }

        AttendanceEntity attendance = new AttendanceEntity
        {
            ApplicantId = request.ApplicantId,
            AttendanceDate = request.AttendanceDate,
            CheckInTime = request.CheckInTime,
            CheckOutTime = request.CheckOutTime,
            CaptureFacePath = request.CaptureFacePath,
            FaceMatchPercentage = request.FaceMatchPercentage,
            FaceVerificationStatus = request.FaceVerificationStatus,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            AttendanceStatus = request.AttendanceStatus,
            CreatedOn = DateTime.UtcNow
        };

        dbContext.Attendances.Add(attendance);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(attendance.AttendanceId);
    }
}
