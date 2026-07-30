using CmScheme.HelpDesk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.HelpDesk.Core.Data.Configurations;

public class SlaEscalationLogConfiguration : IEntityTypeConfiguration<SlaEscalationLog>
{
    public void Configure(EntityTypeBuilder<SlaEscalationLog> builder)
    {
        builder.ToTable("SlaEscalationLogs");
        builder.HasKey(e => e.EscalationLogId);
        builder.Property(e => e.EscalatedTo).HasMaxLength(200);
        builder.Property(e => e.Reason).HasMaxLength(500);
    }
}
