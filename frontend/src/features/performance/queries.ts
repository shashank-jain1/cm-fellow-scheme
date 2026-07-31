import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import {
  fetchPerformanceSummary,
  fetchPerformanceDetail,
  recordSupervisorRating,
  recordEvaluationRemarks,
  calculatePerformanceScore,
  submitReview,
  fetchReviewHistory,
  createGoal,
  fetchGoalsByUser,
  updateGoalStatus,
  createImprovementPlan,
  fetchImprovementPlansByUser,
  submitPeerFeedback,
  submitSelfAssessment,
} from './api';
import type { SubmitReviewRequest, CreateGoalCommand, UpdateGoalStatusCommand, CreateImprovementPlanCommand, SubmitPeerFeedbackCommand, SubmitSelfAssessmentCommand } from './types';

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

export function useGoalsByUser(userId: number) {
  return useQuery({
    queryKey: ['performance', 'goals', userId],
    queryFn: () => fetchGoalsByUser(userId),
    enabled: !!userId,
  });
}

export function useCreateGoal() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: CreateGoalCommand) => createGoal(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['performance', 'goals'] });
    },
  });
}

export function useUpdateGoalStatus() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ goalId, command }: { goalId: number; command: UpdateGoalStatusCommand }) =>
      updateGoalStatus(goalId, command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['performance', 'goals'] });
    },
  });
}

export function useImprovementPlansByUser(userId: number) {
  return useQuery({
    queryKey: ['performance', 'improvement-plans', userId],
    queryFn: () => fetchImprovementPlansByUser(userId),
    enabled: !!userId,
  });
}

export function useCreateImprovementPlan() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: CreateImprovementPlanCommand) => createImprovementPlan(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['performance', 'improvement-plans'] });
    },
  });
}

export function useSubmitPeerFeedback() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: SubmitPeerFeedbackCommand) => submitPeerFeedback(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['performance', 'peer-feedback'] });
    },
  });
}

export function useSubmitSelfAssessment() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: SubmitSelfAssessmentCommand) => submitSelfAssessment(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['performance', 'self-assessment'] });
    },
  });
}
