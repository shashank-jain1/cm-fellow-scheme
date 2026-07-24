using CmScheme.Common.Core.Data;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.AttendanceLeave.Core.Data.Configurations;
using CmScheme.AttendanceLeave.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.AttendanceLeave.Infrastructure;

public class AttendanceLeaveDbContext : BaseDbContext, IAttendanceLeaveCommandDbContext, IAttendanceLeaveQueryDbContext
{
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<LeaveApplication> LeaveApplications => Set<LeaveApplication>();
    public DbSet<LeaveBalance> LeaveBalances => Set<LeaveBalance>();

    public AttendanceLeaveDbContext(DbContextOptions<AttendanceLeaveDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AttendanceConfiguration());
        modelBuilder.ApplyConfiguration(new LeaveApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new LeaveBalanceConfiguration());
    }

    IQueryable<Attendance> IAttendanceLeaveQueryDbContext.Attendances => Attendances;
    IQueryable<LeaveApplication> IAttendanceLeaveQueryDbContext.LeaveApplications => LeaveApplications;
    IQueryable<LeaveBalance> IAttendanceLeaveQueryDbContext.LeaveBalances => LeaveBalances;
}
