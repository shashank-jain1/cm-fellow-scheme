using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using AttendanceEntity = CmScheme.AttendanceLeave.Core.Entities.Attendance;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.MarkAttendance;

public sealed class MarkAttendanceCommandHandler(IAttendanceLeaveCommandDbContext dbContext)
    : ICommandHandler<MarkAttendanceCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(MarkAttendanceCommand request, CancellationToken cancellationToken)
    {
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
