import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { surveyDetailApi, taskProgressApi } from './api';
import type { RecordSurveyPayload } from './api';

export const useRecordSurveySubmission = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ taskProgressId, data }: { taskProgressId: number; data: RecordSurveyPayload }) =>
      taskProgressApi.recordSurveySubmission(taskProgressId, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['task-progress'] });
      queryClient.invalidateQueries({ queryKey: ['survey-details'] });
    },
  });
};

export const useSurveyDetails = (taskProgressId: number) => {
  return useQuery({
    queryKey: ['survey-details', taskProgressId],
    queryFn: async () => {
      const res = await surveyDetailApi.getByTaskProgressId(taskProgressId);
      return res.data ?? [];
    },
    enabled: !!taskProgressId,
  });
};
