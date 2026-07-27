using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Core.Data.Configurations;

public sealed class GramPanchayatConfiguration : IEntityTypeConfiguration<GramPanchayat>
{
    public void Configure(EntityTypeBuilder<GramPanchayat> builder)
    {
        builder.ToTable("GramPanchayat");
        builder.HasKey(g => g.GramPanchayatId);

        builder.Property(g => g.GramPanchayatName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(g => g.GPCode)
            .HasMaxLength(20);

        builder.Property(g => g.IsActive)
            .HasDefaultValue(true);

        builder.HasIndex(g => g.BlockId);

        builder.HasOne(g => g.Block)
            .WithMany()
            .HasForeignKey(g => g.BlockId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
