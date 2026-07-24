using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Core.Data.Configurations;

public sealed class BlockConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.HasKey(b => b.BlockId);

        builder.Property(b => b.BlockName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.BlockCode)
            .HasMaxLength(10);

        builder.Property(b => b.IsActive)
            .HasDefaultValue(true);

        builder.HasIndex(b => b.DistrictId);

        builder.HasOne(b => b.District)
            .WithMany()
            .HasForeignKey(b => b.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
