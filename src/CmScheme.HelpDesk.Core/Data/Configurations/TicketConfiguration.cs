using CmScheme.HelpDesk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.HelpDesk.Core.Data.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");
        builder.HasKey(e => e.TicketId);
        builder.Property(e => e.Email).HasMaxLength(200);
        builder.Property(e => e.Mobile).HasMaxLength(20);
        builder.Property(e => e.IssueCategory).HasMaxLength(100);
        builder.Property(e => e.IssueDescription).HasMaxLength(2000);
        builder.Property(e => e.Priority).HasMaxLength(20);
        builder.Property(e => e.Status).HasMaxLength(50);
        builder.Property(e => e.ResolutionRemarks).HasMaxLength(2000);
    }
}
