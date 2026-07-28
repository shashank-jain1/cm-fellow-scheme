using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Infrastructure.Configurations;

public sealed class TrainingScheduleConfiguration : IEntityTypeConfiguration<TrainingSchedule>
{
    public void Configure(EntityTypeBuilder<TrainingSchedule> builder)
    {
        builder.ToTable("TrainingSchedule");

        builder.HasKey(ts => ts.TrainingScheduleId);

        builder.Property(ts => ts.CalendarYear)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(ts => ts.VenueName)
            .HasMaxLength(250);

        builder.Property(ts => ts.TrainingDescription)
            .HasMaxLength(1000);

        builder.HasOne(ts => ts.Project)
            .WithMany()
            .HasForeignKey(ts => ts.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
