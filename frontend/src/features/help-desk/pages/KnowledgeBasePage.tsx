import { useState } from 'react';
import { useKnowledgeBaseSearch } from '../queries';
import { PageHeader } from '../../../shared/components/ui';
import type { KnowledgeBaseArticleDto } from '../types';
import KnowledgeBaseSearchBar from '../components/KnowledgeBaseSearchBar';
import KnowledgeBaseArticleList from '../components/KnowledgeBaseArticleList';

export default function KnowledgeBasePage() {
  const [searchQuery, setSearchQuery] = useState('');
  const [selectedArticle, setSelectedArticle] = useState<KnowledgeBaseArticleDto | null>(null);
  const { data: articles = [], isLoading } = useKnowledgeBaseSearch(searchQuery || undefined);

  return (
    <div>
      <PageHeader title="Knowledge Base" subtitle="Search articles and guides for self-service support" />
      <KnowledgeBaseSearchBar searchQuery={searchQuery} setSearchQuery={setSearchQuery} />
      <KnowledgeBaseArticleList
        articles={articles} isLoading={isLoading} searchQuery={searchQuery}
        onSelect={setSelectedArticle} selectedArticle={selectedArticle} onBack={() => setSelectedArticle(null)}
      />
    </div>
  );
}
