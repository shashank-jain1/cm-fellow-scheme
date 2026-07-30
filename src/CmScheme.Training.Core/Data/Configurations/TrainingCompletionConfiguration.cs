using CmScheme.Training.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.Training.Core.Data.Configurations;

public sealed class TrainingCompletionConfiguration : IEntityTypeConfiguration<TrainingCompletion>
{
    public void Configure(EntityTypeBuilder<TrainingCompletion> builder)
    {
        builder.ToTable("TrainingCompletions");
        builder.HasKey(e => e.TrainingCompletionId);
        builder.Property(e => e.Status).HasMaxLength(20).IsRequired();
        builder.Property(e => e.FeedbackComments).HasMaxLength(2000);
        builder.HasOne<TrainingSchedule>()
            .WithMany()
            .HasForeignKey(e => e.TrainingScheduleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
