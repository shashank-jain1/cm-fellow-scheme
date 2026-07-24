using CmScheme.AttendanceLeave.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.AttendanceLeave.Core.Data;

public interface IAttendanceLeaveQueryDbContext
{
    IQueryable<Attendance> Attendances { get; }
    IQueryable<LeaveApplication> LeaveApplications { get; }
    IQueryable<LeaveBalance> LeaveBalances { get; }
}
