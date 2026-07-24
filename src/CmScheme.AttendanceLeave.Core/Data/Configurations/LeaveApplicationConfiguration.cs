using CmScheme.AttendanceLeave.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.AttendanceLeave.Core.Data.Configurations;

public class LeaveApplicationConfiguration : IEntityTypeConfiguration<LeaveApplication>
{
    public void Configure(EntityTypeBuilder<LeaveApplication> builder)
    {
        builder.HasKey(e => e.LeaveApplicationId);
        builder.Property(e => e.LeaveType).HasMaxLength(100);
        builder.Property(e => e.HalfDayFullDay).HasMaxLength(20);
        builder.Property(e => e.LeaveReason).HasMaxLength(1000);
        builder.Property(e => e.AttachmentPath).HasMaxLength(500);
        builder.Property(e => e.ReportingManagerName).HasMaxLength(200);
        builder.Property(e => e.Status).HasMaxLength(50);
        builder.Property(e => e.CreatedBy).HasMaxLength(200);
    }
}
