using CmScheme.WorkAllocation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.WorkAllocation.Core.Data.Configurations;

public sealed class SurveyRecordConfiguration : IEntityTypeConfiguration<SurveyRecord>
{
    public void Configure(EntityTypeBuilder<SurveyRecord> builder)
    {
        builder.HasKey(e => e.SurveyRecordId);
        builder.Property(e => e.InternName).HasMaxLength(200);
        builder.Property(e => e.SurveyPersonName).HasMaxLength(200);
        builder.Property(e => e.MobileNumber).HasMaxLength(20);
        builder.Property(e => e.PanchayatName).HasMaxLength(200);
        builder.Property(e => e.VillageName).HasMaxLength(200);
        builder.Property(e => e.SurveyStatus).HasMaxLength(50);
        builder.Property(e => e.Latitude).HasPrecision(10, 7);
        builder.Property(e => e.Longitude).HasPrecision(10, 7);
    }
}
