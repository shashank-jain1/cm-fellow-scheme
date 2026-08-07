import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { taskProgressApi, taskDependencyApi, taskAttachmentApi } from './api';
import type { TaskDependencyFormData } from './types';

export const useTaskProgress = () => {
  return useQuery({
    queryKey: ['task-progress'],
    queryFn: async () => {
      const res = await taskProgressApi.getAll();
      return res.data ?? [];
    },
  });
};

export const useTaskProgressByWorkAllocation = (workAllocationId: number) => {
  return useQuery({
    queryKey: ['task-progress', 'work-allocation', workAllocationId],
    queryFn: async () => {
      const res = await taskProgressApi.getByWorkAllocationId(workAllocationId);
      return res.data ?? [];
    },
    enabled: !!workAllocationId,
  });
};

export const useTaskDependenciesByWorkAllocation = (workAllocationId: number) => {
  return useQuery({
    queryKey: ['task-dependencies', 'work-allocation', workAllocationId],
    queryFn: async () => {
      const res = await taskDependencyApi.getByWorkAllocationId(workAllocationId);
      return res.data ?? [];
    },
    enabled: !!workAllocationId,
  });
};

export const useCreateTaskDependency = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (data: TaskDependencyFormData) => taskDependencyApi.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['task-dependencies'] });
    },
  });
};

export const useTaskAttachments = (workAllocationId: number) => {
  return useQuery({
    queryKey: ['task-attachments', workAllocationId],
    queryFn: async () => {
      const res = await taskAttachmentApi.getByWorkAllocationId(workAllocationId);
      return res.data ?? [];
    },
    enabled: !!workAllocationId,
  });
};

export const useUploadTaskAttachment = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ workAllocationId, file }: { workAllocationId: number; file: File }) =>
      taskAttachmentApi.upload(workAllocationId, file),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['task-attachments'] });
    },
  });
};
