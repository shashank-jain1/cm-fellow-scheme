using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Training.Core.Entities;

namespace CmScheme.Training.Core.Data.Configurations;

public sealed class TrainingScheduleConfiguration : IEntityTypeConfiguration<TrainingSchedule>
{
    public void Configure(EntityTypeBuilder<TrainingSchedule> builder)
    {
        builder.ToTable("TrainingSchedules");
        builder.HasKey(x => x.TrainingScheduleId);
        builder.Property(x => x.ActivityType).HasMaxLength(20);
        builder.Property(x => x.ActivityTitle).HasMaxLength(250);
        builder.Property(x => x.TrainingTitle).HasMaxLength(250);
        builder.Property(x => x.MeetingTitle).HasMaxLength(250);
        builder.Property(x => x.Mode).HasMaxLength(50);
        builder.Property(x => x.TrainingCategory).HasMaxLength(20);
        builder.Property(x => x.TrainerName).HasMaxLength(100);
        builder.Property(x => x.TrainerMobile).HasMaxLength(10);
        builder.Property(x => x.Remarks).HasMaxLength(2000);
        builder.Property(x => x.MaterialPath).HasMaxLength(255);
        builder.Property(x => x.Status).HasMaxLength(20);
    }
}
