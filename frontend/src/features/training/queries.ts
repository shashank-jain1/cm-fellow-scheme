import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { fetchTrainingSessions, fetchTrainingMeetings, createTrainingSession, createTrainingMeeting, updateTrainingStatus } from './api';
import type { ActivityFormData } from './types';

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
