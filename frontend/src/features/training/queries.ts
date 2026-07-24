import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { fetchTrainingSchedules, fetchTrainingScheduleDetail, createTrainingSchedule, updateTrainingSchedule, deleteTrainingSchedule } from './api';
import type { ActivityFormData } from './types';

export function useTrainingSchedules() {
  return useQuery({
    queryKey: ['training-schedules'],
    queryFn: fetchTrainingSchedules,
  });
}

export function useTrainingScheduleDetail(id: number) {
  return useQuery({
    queryKey: ['training-schedule', id],
    queryFn: () => fetchTrainingScheduleDetail(id),
    enabled: !!id,
  });
}

export function useCreateTrainingSchedule() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: ActivityFormData) => createTrainingSchedule(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['training-schedules'] });
    },
  });
}

export function useUpdateTrainingSchedule() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, command }: { id: number; command: ActivityFormData }) =>
      updateTrainingSchedule(id, command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['training-schedules'] });
    },
  });
}

export function useDeleteTrainingSchedule() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => deleteTrainingSchedule(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['training-schedules'] });
    },
  });
}
