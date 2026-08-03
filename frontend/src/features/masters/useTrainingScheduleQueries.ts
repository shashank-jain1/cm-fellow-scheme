import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { mastersApi } from './api';

export function useTrainingSchedules(filters?: { calendarYear?: string; projectId?: number; divisionId?: number }) {
  return useQuery({
    queryKey: ['masters', 'trainingSchedules', filters],
    queryFn: async () => {
      const res = await mastersApi.getTrainingSchedules(filters);
      return res.data ?? [];
    },
  });
}

export function useTrainingSchedule(id: number) {
  return useQuery({
    queryKey: ['masters', 'trainingSchedules', id],
    queryFn: async () => {
      const res = await mastersApi.getTrainingSchedule(id);
      return res.data;
    },
    enabled: id > 0,
  });
}

export function useCreateTrainingSchedule() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: mastersApi.createTrainingSchedule,
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'trainingSchedules'] }),
  });
}

export function useUpdateTrainingSchedule() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: number; data: Parameters<typeof mastersApi.updateTrainingSchedule>[1] }) =>
      mastersApi.updateTrainingSchedule(id, data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'trainingSchedules'] }),
  });
}
