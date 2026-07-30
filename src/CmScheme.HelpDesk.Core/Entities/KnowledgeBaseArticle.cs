namespace CmScheme.HelpDesk.Core.Entities;

public class KnowledgeBaseArticle
{
    public int KnowledgeBaseArticleId { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string? Tags { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = null!;
    public int ViewCount { get; set; }
    public bool IsPublished { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
