import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { fetchPerformanceSummary, fetchPerformanceDetail, recordSupervisorRating, recordEvaluationRemarks, calculatePerformanceScore } from './api';

export function usePerformanceSummary() {
  return useQuery({
    queryKey: ['performance', 'summary'],
    queryFn: fetchPerformanceSummary,
  });
}

export function usePerformanceDetail(id: number) {
  return useQuery({
    queryKey: ['performance', 'detail', id],
    queryFn: () => fetchPerformanceDetail(id),
    enabled: !!id,
  });
}

export function useRecordSupervisorRating() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: recordSupervisorRating,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['performance'] });
    },
  });
}

export function useRecordEvaluationRemarks() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: recordEvaluationRemarks,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['performance'] });
    },
  });
}

export function useCalculatePerformanceScore() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (performanceEvaluationId: number) => calculatePerformanceScore(performanceEvaluationId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['performance'] });
    },
  });
}
