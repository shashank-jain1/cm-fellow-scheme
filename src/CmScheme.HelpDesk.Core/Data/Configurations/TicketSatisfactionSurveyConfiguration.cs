using CmScheme.HelpDesk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.HelpDesk.Core.Data.Configurations;

public class TicketSatisfactionSurveyConfiguration : IEntityTypeConfiguration<TicketSatisfactionSurvey>
{
    public void Configure(EntityTypeBuilder<TicketSatisfactionSurvey> builder)
    {
        builder.ToTable("TicketSatisfactionSurveys");
        builder.HasKey(e => e.SurveyId);
        builder.Property(e => e.Comments).HasMaxLength(500);
    }
}
