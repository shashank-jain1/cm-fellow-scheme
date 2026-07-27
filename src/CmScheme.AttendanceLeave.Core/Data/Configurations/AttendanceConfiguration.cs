using CmScheme.AttendanceLeave.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.AttendanceLeave.Core.Data.Configurations;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.ToTable("Attendances");
        builder.HasKey(e => e.AttendanceId);
        builder.Property(e => e.CaptureFacePath).HasMaxLength(500);
        builder.Property(e => e.FaceVerificationStatus).HasMaxLength(50);
        builder.Property(e => e.AttendanceStatus).HasMaxLength(50);
    }
}
