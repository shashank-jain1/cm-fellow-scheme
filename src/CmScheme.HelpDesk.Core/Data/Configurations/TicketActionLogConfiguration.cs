using CmScheme.HelpDesk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.HelpDesk.Core.Data.Configurations;

public class TicketActionLogConfiguration : IEntityTypeConfiguration<TicketActionLog>
{
    public void Configure(EntityTypeBuilder<TicketActionLog> builder)
    {
        builder.HasKey(e => e.TicketActionLogId);
        builder.Property(e => e.ActionBy).HasMaxLength(200);
        builder.Property(e => e.ActionType).HasMaxLength(50);
        builder.Property(e => e.Remarks).HasMaxLength(2000);
    }
}
