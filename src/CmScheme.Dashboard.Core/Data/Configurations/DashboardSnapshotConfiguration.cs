using CmScheme.Dashboard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.Dashboard.Core.Data.Configurations;

public class DashboardSnapshotConfiguration : IEntityTypeConfiguration<DashboardSnapshot>
{
    public void Configure(EntityTypeBuilder<DashboardSnapshot> builder)
    {
        builder.ToTable("DashboardSnapshots");
        builder.HasKey(e => e.SnapshotId);
        builder.Property(e => e.JsonData).HasMaxLength(4000);
    }
}
