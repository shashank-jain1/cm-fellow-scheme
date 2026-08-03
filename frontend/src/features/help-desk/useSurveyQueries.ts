import { useMutation, useQueryClient } from '@tanstack/react-query';
import { submitSurvey } from './api';
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
