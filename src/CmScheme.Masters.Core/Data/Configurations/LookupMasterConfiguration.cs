using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Core.Data.Configurations;

public sealed class LookupMasterConfiguration : IEntityTypeConfiguration<LookupMaster>
{
    public void Configure(EntityTypeBuilder<LookupMaster> builder)
    {
        builder.ToTable("LookupMaster");
        builder.HasKey(lm => lm.LookupMasterId);

        builder.Property(lm => lm.MasterType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(lm => lm.Label)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(lm => lm.Value)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(lm => new { lm.MasterType, lm.Value }).IsUnique();
    }
}
