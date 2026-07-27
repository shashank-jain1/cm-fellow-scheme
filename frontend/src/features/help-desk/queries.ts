import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { fetchTickets, fetchTicketDetail, raiseTicket, resolveTicket, escalateTicket } from './api';
import type { TicketFormData } from './types';

export function useTickets(role?: string, applicantId?: number) {
  return useQuery({
    queryKey: ['helpdesk-tickets', role, applicantId],
    queryFn: () => fetchTickets(role, applicantId),
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
