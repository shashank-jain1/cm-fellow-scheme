using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Core.Data.Configurations;

public sealed class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.HasKey(d => d.DistrictId);

        builder.Property(d => d.DistrictName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.DistrictCode)
            .HasMaxLength(10);

        builder.Property(d => d.IsActive)
            .HasDefaultValue(true);

        builder.HasIndex(d => d.DivisionId);

        builder.HasOne(d => d.Division)
            .WithMany()
            .HasForeignKey(d => d.DivisionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
