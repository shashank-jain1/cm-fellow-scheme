using CmScheme.AttendanceLeave.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.AttendanceLeave.Core.Data.Configurations;

public class LeaveBalanceConfiguration : IEntityTypeConfiguration<LeaveBalance>
{
    public void Configure(EntityTypeBuilder<LeaveBalance> builder)
    {
        builder.ToTable("LeaveBalances");
        builder.HasKey(e => e.LeaveBalanceId);
        builder.Property(e => e.LeaveType).HasMaxLength(100);
    }
}
