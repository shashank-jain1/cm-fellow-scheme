using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.AttendanceLeave.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.AttendanceLeave.Infrastructure;

public class AttendanceLeaveCommandDbContext : IAttendanceLeaveCommandDbContext
{
    private readonly AttendanceLeaveDbContext _dbContext;

    public AttendanceLeaveCommandDbContext(AttendanceLeaveDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbSet<Attendance> Attendances => _dbContext.Attendances;
    public DbSet<LeaveApplication> LeaveApplications => _dbContext.LeaveApplications;
    public DbSet<LeaveBalance> LeaveBalances => _dbContext.LeaveBalances;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
