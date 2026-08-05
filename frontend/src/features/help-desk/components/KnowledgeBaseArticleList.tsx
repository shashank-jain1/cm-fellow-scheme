import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
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
      <DataTable
        value={articles}
        responsiveLayout="scroll"
        emptyMessage="No articles found"
        dataKey="articleId"
        onRowClick={(e) => onSelect(e.data as KnowledgeBaseArticleDto)}
        style={{ cursor: 'pointer' }}
      >
        <Column field="title" header="Title" bodyStyle={{ fontWeight: 600, fontSize: 14, color: 'var(--accent-primary)' }} />
        <Column field="category" header="Category" bodyStyle={{ fontSize: 13, color: 'var(--text-secondary)' }} />
        <Column field="authorName" header="Author" bodyStyle={{ fontSize: 13, color: 'var(--text-secondary)' }} body={(row: KnowledgeBaseArticleDto) => row.authorName ?? '-'} />
        <Column
          header="Published"
          body={(row: KnowledgeBaseArticleDto) => (
            <span className="badge" style={{ background: row.published ? 'var(--badge-emerald-bg)' : 'var(--badge-amber-bg)', color: row.published ? 'var(--badge-emerald-text)' : 'var(--badge-amber-text)' }}>
              {row.published ? 'Published' : 'Draft'}
            </span>
          )}
        />
      </DataTable>
    </div>
  );
}
