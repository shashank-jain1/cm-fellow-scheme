import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { searchKnowledgeBase, fetchKnowledgeBaseArticle, createArticle } from './api';
import type { CreateKBArticleCommand } from './types';

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

export function useCreateKBArticle() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: CreateKBArticleCommand) => createArticle(data),
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ['helpdesk-knowledge-base'] });
    },
  });
}
