using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.AttendanceLeave.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.AttendanceLeave.Infrastructure;

public class AttendanceLeaveQueryDbContext : IAttendanceLeaveQueryDbContext
{
    private readonly AttendanceLeaveDbContext _dbContext;

    public AttendanceLeaveQueryDbContext(AttendanceLeaveDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Attendance> Attendances => _dbContext.Attendances;
    public IQueryable<LeaveApplication> LeaveApplications => _dbContext.LeaveApplications;
    public IQueryable<LeaveBalance> LeaveBalances => _dbContext.LeaveBalances;
}
