using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data.Configurations;

public sealed class TicketCategoryConfiguration : IEntityTypeConfiguration<TicketCategory>
{
    public void Configure(EntityTypeBuilder<TicketCategory> builder)
    {
        builder.ToTable("TicketCategories");
        builder.HasKey(tc => tc.TicketCategoryId);

        builder.Property(tc => tc.CategoryName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(tc => tc.Description)
            .HasMaxLength(500);

        builder.Property(tc => tc.DefaultPriority)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(tc => tc.IsActive)
            .HasDefaultValue(true);

        builder.Property(tc => tc.SortOrder);

        builder.Property(tc => tc.CreatedOn);
    }
}
