import { useQuery } from '@tanstack/react-query';
import { searchKnowledgeBase, fetchKnowledgeBaseArticle } from './api';

export function useKnowledgeBaseSearch(query?: string, category?: string) {
  return useQuery({
    queryKey: ['helpdesk-knowledge-base', query, category],
    queryFn: () => searchKnowledgeBase(query, category),
  });
}

export function useKnowledgeBaseArticle(id: number) {
  return useQuery({
    queryKey: ['helpdesk-knowledge-base', id],
    queryFn: () => fetchKnowledgeBaseArticle(id),
    enabled: !!id,
  });
}
