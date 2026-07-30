using CmScheme.HelpDesk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.HelpDesk.Core.Data.Configurations;

public class KnowledgeBaseArticleConfiguration : IEntityTypeConfiguration<KnowledgeBaseArticle>
{
    public void Configure(EntityTypeBuilder<KnowledgeBaseArticle> builder)
    {
        builder.ToTable("KnowledgeBaseArticles");
        builder.HasKey(e => e.KnowledgeBaseArticleId);
        builder.Property(e => e.Title).HasMaxLength(500);
        builder.Property(e => e.Content).HasMaxLength(5000);
        builder.Property(e => e.Category).HasMaxLength(100);
        builder.Property(e => e.Tags).HasMaxLength(500);
        builder.Property(e => e.AuthorName).HasMaxLength(200);
        builder.Property(e => e.IsPublished).HasDefaultValue(false);
        builder.Property(e => e.IsActive).HasDefaultValue(true);
        builder.Property(e => e.ViewCount).HasDefaultValue(0);
    }
}
