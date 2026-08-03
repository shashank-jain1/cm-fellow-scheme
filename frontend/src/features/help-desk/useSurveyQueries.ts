import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { submitSurvey, getSurvey } from './api';
import type { SubmitSurveyCommand } from './types';

export function useSubmitSurvey() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: SubmitSurveyCommand) => submitSurvey(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['helpdesk-survey'] });
    },
  });
}

export function useGetSurvey(ticketId: number) {
  return useQuery({
    queryKey: ['helpdesk-survey', ticketId],
    queryFn: () => getSurvey(ticketId),
    enabled: !!ticketId,
  });
}
