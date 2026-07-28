import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { fetchPerformanceSummary, fetchPerformanceDetail, recordSupervisorRating, recordEvaluationRemarks, calculatePerformanceScore, submitReview, fetchReviewHistory } from './api';
import type { SubmitReviewRequest } from './types';

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

export function useSubmitReview() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, command }: { id: number; command: SubmitReviewRequest }) => submitReview(id, command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['performance'] });
    },
  });
}

export function useReviewHistory(id: number) {
  return useQuery({
    queryKey: ['performance', 'review-history', id],
    queryFn: () => fetchReviewHistory(id),
    enabled: !!id,
  });
}
