using CmScheme.HelpDesk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.HelpDesk.Core.Data.Configurations;

public class SlaPolicyConfiguration : IEntityTypeConfiguration<SlaPolicy>
{
    public void Configure(EntityTypeBuilder<SlaPolicy> builder)
    {
        builder.ToTable("SlaPolicies");
        builder.HasKey(e => e.SlaPolicyId);
        builder.Property(e => e.PriorityLevel).HasMaxLength(20);
        builder.Property(e => e.EscalationEmails).HasMaxLength(500);
        builder.Property(e => e.IsActive).HasDefaultValue(true);
    }
}
