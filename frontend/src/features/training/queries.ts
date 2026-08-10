import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import {
  fetchTrainingSessions,
  fetchTrainingMeetings,
  createTrainingSession,
  createTrainingMeeting,
  updateTrainingStatus,
  fetchTrainingCompletions,
  createTrainingCompletion,
  fetchSessionMaterials,
  uploadTrainingMaterial,
  uploadMeetingAttachment,
  uploadMom,
} from './api';
import type { ActivityFormData, TrainingCompletionFormData } from './types';

export function useTrainingSessions() {
  return useQuery({
    queryKey: ['training-sessions'],
    queryFn: fetchTrainingSessions,
  });
}

export function useTrainingMeetings() {
  return useQuery({
    queryKey: ['training-meetings'],
    queryFn: fetchTrainingMeetings,
  });
}

export function useCreateTrainingSession() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: ActivityFormData) => createTrainingSession(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['training-sessions'] });
    },
  });
}

export function useCreateTrainingMeeting() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: ActivityFormData) => createTrainingMeeting(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['training-meetings'] });
    },
  });
}

export function useUpdateTrainingStatus() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ trainingScheduleId, newStatus }: { trainingScheduleId: number; newStatus: string }) =>
      updateTrainingStatus(trainingScheduleId, newStatus),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['training-sessions'] });
      queryClient.invalidateQueries({ queryKey: ['training-meetings'] });
    },
  });
}

export function useTrainingCompletions() {
  return useQuery({
    queryKey: ['training-completions'],
    queryFn: fetchTrainingCompletions,
  });
}

export function useCreateTrainingCompletion() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: TrainingCompletionFormData) => createTrainingCompletion(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['training-completions'] });
    },
  });
}

export function useSessionMaterials(trainingScheduleId: number) {
  return useQuery({
    queryKey: ['training-materials', trainingScheduleId],
    queryFn: () => fetchSessionMaterials(trainingScheduleId),
    enabled: !!trainingScheduleId,
  });
}

export function useUploadTrainingMaterial() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ trainingScheduleId, file }: { trainingScheduleId: number; file: File }) =>
      uploadTrainingMaterial(trainingScheduleId, file),
    onSuccess: (_data, variables) => {
      queryClient.invalidateQueries({ queryKey: ['training-materials', variables.trainingScheduleId] });
    },
  });
}

export function useUploadMeetingAttachment() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ trainingScheduleId, file }: { trainingScheduleId: number; file: File }) =>
      uploadMeetingAttachment(trainingScheduleId, file),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['training-meetings'] });
    },
  });
}

export function useUploadMom() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ trainingScheduleId, file }: { trainingScheduleId: number; file: File }) =>
      uploadMom(trainingScheduleId, file),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['training-meetings'] });
    },
  });
}
