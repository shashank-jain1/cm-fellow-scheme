import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import {
  fetchImprovementPlansByUser,
  createImprovementPlan,
  submitPeerFeedback,
  submitSelfAssessment,
  getPeerFeedback,
  getSelfAssessment,
} from './api';
import type {
  CreateImprovementPlanCommand,
  SubmitPeerFeedbackCommand,
  SubmitSelfAssessmentCommand,
} from './types';

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

export function usePeerFeedback(userId: number) {
  return useQuery({
    queryKey: ['performance', 'peer-feedback', userId],
    queryFn: () => getPeerFeedback(userId),
    enabled: !!userId,
  });
}

export function useSelfAssessment() {
  return useQuery({
    queryKey: ['performance', 'self-assessment', 'current'],
    queryFn: getSelfAssessment,
  });
}
