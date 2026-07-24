import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { workAllocationApi, taskProgressApi, surveyDetailApi } from './api';
import type { WorkAllocationFormData } from './types';

export const useWorkAllocations = () => {
  return useQuery({
    queryKey: ['work-allocations'],
    queryFn: async () => {
      const res = await workAllocationApi.getAll();
      return res.data ?? [];
    },
  });
};

export const useWorkAllocation = (id: number) => {
  return useQuery({
    queryKey: ['work-allocation', id],
    queryFn: async () => {
      const res = await workAllocationApi.getById(id);
      return res.data;
    },
    enabled: !!id,
  });
};

export const useCreateWorkAllocation = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (data: WorkAllocationFormData) => workAllocationApi.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['work-allocations'] });
    },
  });
};

export const useUpdateWorkAllocation = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: number; data: WorkAllocationFormData }) =>
      workAllocationApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['work-allocations'] });
    },
  });
};

export const useDeleteWorkAllocation = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => workAllocationApi.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['work-allocations'] });
    },
  });
};

export const useTaskProgress = () => {
  return useQuery({
    queryKey: ['task-progress'],
    queryFn: async () => {
      const res = await taskProgressApi.getAll();
      return res.data ?? [];
    },
  });
};

export const useTaskProgressByProject = (projectId: number) => {
  return useQuery({
    queryKey: ['task-progress', 'project', projectId],
    queryFn: async () => {
      const res = await taskProgressApi.getByProjectId(projectId);
      return res.data ?? [];
    },
    enabled: !!projectId,
  });
};

export const useSurveyDetails = (workProjectId: number) => {
  return useQuery({
    queryKey: ['survey-details', workProjectId],
    queryFn: async () => {
      const res = await surveyDetailApi.getByWorkProjectId(workProjectId);
      return res.data ?? [];
    },
    enabled: !!workProjectId,
  });
};
