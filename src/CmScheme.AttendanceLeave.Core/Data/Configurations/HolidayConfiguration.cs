using CmScheme.AttendanceLeave.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.AttendanceLeave.Core.Data.Configurations;

public sealed class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
{
    public void Configure(EntityTypeBuilder<Holiday> builder)
    {
        builder.ToTable("Holidays");
        builder.HasKey(h => h.HolidayId);
        builder.Property(h => h.HolidayName).IsRequired().HasMaxLength(200);
        builder.Property(h => h.HolidayDate).IsRequired();
        builder.Property(h => h.Description).HasMaxLength(500);
        builder.Property(h => h.IsOptional).HasDefaultValue(false);
        builder.Property(h => h.IsActive).HasDefaultValue(true);
    }
}
