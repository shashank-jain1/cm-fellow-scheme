import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import {
  fetchTickets,
  fetchTicketDetail,
  raiseTicket,
  resolveTicket,
  escalateTicket,
  closeTicket,
  listTicketsByRole,
  submitSurvey,
  searchKnowledgeBase,
  fetchKnowledgeBaseArticle,
} from './api';
import type { TicketFormData, SubmitSurveyCommand } from './types';

export function useTickets(role?: string, applicantId?: number) {
  return useQuery({
    queryKey: ['helpdesk-tickets', role, applicantId],
    queryFn: () => fetchTickets(role, applicantId),
  });
}

export function useListTicketsByRoleQuery(role: string, applicantId?: number) {
  return useQuery({
    queryKey: ['helpdesk-tickets', role, applicantId],
    queryFn: () => listTicketsByRole(role, applicantId),
  });
}

export function useTicketDetail(id: number) {
  return useQuery({
    queryKey: ['helpdesk-ticket', id],
    queryFn: () => fetchTicketDetail(id),
    enabled: !!id,
  });
}

export function useRaiseTicket() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: TicketFormData) => raiseTicket(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['helpdesk-tickets'] });
    },
  });
}

export function useResolveTicket() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, resolutionRemarks }: { id: number; resolutionRemarks: string }) =>
      resolveTicket(id, resolutionRemarks),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['helpdesk-tickets'] });
      queryClient.invalidateQueries({ queryKey: ['helpdesk-ticket'] });
    },
  });
}

export function useEscalateTicket() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => escalateTicket(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['helpdesk-tickets'] });
      queryClient.invalidateQueries({ queryKey: ['helpdesk-ticket'] });
    },
  });
}

export function useCloseTicket() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, resolutionRemarks }: { id: number; resolutionRemarks: string }) =>
      closeTicket(id, resolutionRemarks),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['helpdesk-tickets'] });
      queryClient.invalidateQueries({ queryKey: ['helpdesk-ticket'] });
    },
  });
}

export function useSubmitSurvey() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: SubmitSurveyCommand) => submitSurvey(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['helpdesk-survey'] });
    },
  });
}

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
