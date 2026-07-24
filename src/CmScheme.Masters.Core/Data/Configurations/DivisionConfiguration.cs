using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Core.Data.Configurations;

public sealed class DivisionConfiguration : IEntityTypeConfiguration<Division>
{
    public void Configure(EntityTypeBuilder<Division> builder)
    {
        builder.HasKey(d => d.DivisionId);

        builder.Property(d => d.DivisionName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.DivisionCode)
            .HasMaxLength(10);

        builder.Property(d => d.IsActive)
            .HasDefaultValue(true);

        builder.HasIndex(d => d.StateId);

        builder.HasOne(d => d.State)
            .WithMany()
            .HasForeignKey(d => d.StateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
