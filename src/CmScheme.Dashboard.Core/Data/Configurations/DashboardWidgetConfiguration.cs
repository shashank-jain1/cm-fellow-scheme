using CmScheme.Dashboard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.Dashboard.Core.Data.Configurations;

public class DashboardWidgetConfiguration : IEntityTypeConfiguration<DashboardWidget>
{
    public void Configure(EntityTypeBuilder<DashboardWidget> builder)
    {
        builder.ToTable("DashboardWidgets");
        builder.HasKey(e => e.WidgetId);
        builder.Property(e => e.WidgetName).HasMaxLength(100);
        builder.Property(e => e.WidgetType).HasMaxLength(20);
        builder.Property(e => e.DataSourceQuery).HasMaxLength(1000);
        builder.Property(e => e.RoleAccess).HasMaxLength(200);
        builder.Property(e => e.IsActive).HasDefaultValue(true);
    }
}
