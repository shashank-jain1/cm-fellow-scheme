import { AppButton, EmptyState, SkeletonTable } from '../../../shared/components/ui';
import type { KnowledgeBaseArticleDto } from '../types';

interface KnowledgeBaseArticleListProps {
  articles: KnowledgeBaseArticleDto[];
  isLoading: boolean;
  searchQuery: string;
  onSelect: (article: KnowledgeBaseArticleDto) => void;
  selectedArticle: KnowledgeBaseArticleDto | null;
  onBack: () => void;
}

export default function KnowledgeBaseArticleList({ articles, isLoading, searchQuery, onSelect, selectedArticle, onBack }: KnowledgeBaseArticleListProps) {
  if (selectedArticle) {
    return (
      <div className="card" style={{ padding: 24 }}>
        <AppButton variant="ghost" size="sm" icon="pi pi-arrow-left" onClick={onBack} style={{ marginBottom: 16 }}>Back to results</AppButton>
        <h2 style={{ fontSize: 20, fontWeight: 700, marginBottom: 8 }}>{selectedArticle.title}</h2>
        <div style={{ display: 'flex', gap: 12, marginBottom: 16, fontSize: 13, color: 'var(--text-muted)' }}>
          <span>Category: {selectedArticle.category}</span>
          {selectedArticle.authorName && <span>By: {selectedArticle.authorName}</span>}
          <span>{new Date(selectedArticle.createdOn).toLocaleDateString()}</span>
        </div>
        <div style={{ fontSize: 14, lineHeight: 1.7, color: 'var(--text-body)' }} dangerouslySetInnerHTML={{ __html: selectedArticle.content }} />
      </div>
    );
  }

  if (isLoading) return <SkeletonTable columns={4} />;

  if (articles.length === 0) {
    return <EmptyState icon="pi pi-book" title="No articles found" description={searchQuery ? 'Try a different search term' : 'Knowledge base articles will appear here'} />;
  }

  return (
    <div className="table-wrapper">
      <table style={{ width: '100%', borderCollapse: 'collapse' }}>
        <thead>
          <tr>
            {['Title', 'Category', 'Author', 'Published'].map((h) => (
              <th key={h} style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', letterSpacing: '0.5px', borderBottom: '1px solid var(--border-color)' }}>{h}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {articles.map((a) => (
            <tr key={a.articleId} style={{ borderBottom: '1px solid var(--border-light)', cursor: 'pointer' }} onClick={() => onSelect(a)}>
              <td style={{ padding: '14px 16px', fontWeight: 600, fontSize: 14, color: 'var(--accent-primary)' }}>{a.title}</td>
              <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{a.category}</td>
              <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{a.authorName ?? '-'}</td>
              <td style={{ padding: '14px 16px' }}>
                <span className="badge" style={{ background: a.published ? 'var(--badge-emerald-bg)' : 'var(--badge-amber-bg)', color: a.published ? 'var(--badge-emerald-text)' : 'var(--badge-amber-text)' }}>
                  {a.published ? 'Published' : 'Draft'}
                </span>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
