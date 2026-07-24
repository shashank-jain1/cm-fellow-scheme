using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.AttendanceLeave.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.GetAttendanceHistory;

public sealed class GetAttendanceHistoryQueryHandler(IAttendanceLeaveQueryDbContext dbContext)
    : IQueryHandler<GetAttendanceHistoryQuery, Result<IReadOnlyList<AttendanceDto>>>
{
    public async ValueTask<Result<IReadOnlyList<AttendanceDto>>> Handle(GetAttendanceHistoryQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<AttendanceDto> records = await dbContext.Attendances
            .Where(a => a.ApplicantId == request.ApplicantId && a.AttendanceDate >= request.FromDate && a.AttendanceDate <= request.ToDate)
            .Select(a => new AttendanceDto
            {
                AttendanceId = a.AttendanceId,
                ApplicantId = a.ApplicantId,
                AttendanceDate = a.AttendanceDate,
                CheckInTime = a.CheckInTime,
                CheckOutTime = a.CheckOutTime,
                CaptureFacePath = a.CaptureFacePath,
                FaceMatchPercentage = a.FaceMatchPercentage,
                FaceVerificationStatus = a.FaceVerificationStatus,
                Latitude = a.Latitude,
                Longitude = a.Longitude,
                AttendanceStatus = a.AttendanceStatus,
                CreatedOn = a.CreatedOn
            })
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyList<AttendanceDto>>.Success(records);
    }
}
