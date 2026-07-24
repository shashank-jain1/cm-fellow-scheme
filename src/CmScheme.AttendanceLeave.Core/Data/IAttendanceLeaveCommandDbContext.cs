using CmScheme.AttendanceLeave.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.AttendanceLeave.Core.Data;

public interface IAttendanceLeaveCommandDbContext
{
    DbSet<Attendance> Attendances { get; }
    DbSet<LeaveApplication> LeaveApplications { get; }
    DbSet<LeaveBalance> LeaveBalances { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
