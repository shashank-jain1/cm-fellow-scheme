using CmScheme.Performance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.Performance.Core.Data.Configurations;

public class PeerFeedbackConfiguration : IEntityTypeConfiguration<PeerFeedback>
{
    public void Configure(EntityTypeBuilder<PeerFeedback> builder)
    {
        builder.ToTable("PeerFeedbacks");
        builder.HasKey(e => e.PeerFeedbackId);
        builder.Property(e => e.ReviewerName).HasMaxLength(200);
        builder.Property(e => e.RevieweeName).HasMaxLength(200);
        builder.Property(e => e.TechnicalSkillsRating).HasColumnType("decimal(5,2)");
        builder.Property(e => e.CommunicationRating).HasColumnType("decimal(5,2)");
        builder.Property(e => e.TeamworkRating).HasColumnType("decimal(5,2)");
        builder.Property(e => e.ProblemSolvingRating).HasColumnType("decimal(5,2)");
        builder.Property(e => e.LeadershipRating).HasColumnType("decimal(5,2)");
        builder.Property(e => e.OverallRating).HasColumnType("decimal(5,2)");
        builder.Property(e => e.Strengths).HasMaxLength(2000);
        builder.Property(e => e.AreasForImprovement).HasMaxLength(2000);
        builder.Property(e => e.AdditionalComments).HasMaxLength(2000);
        builder.Property(e => e.IsAnonymized).HasDefaultValue(false);
        builder.Property(e => e.Status).HasMaxLength(50).HasDefaultValue("Submitted");
    }
}
