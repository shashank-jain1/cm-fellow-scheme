import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { workAllocationApi, taskProgressApi, surveyDetailApi, taskDependencyApi, taskAttachmentApi } from './api';
import type { WorkAllocationFormData } from './types';
import type { RecordSurveyPayload } from './api';
import type { TaskDependencyFormData } from './types';

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

export const useAssignWorkAllocation = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, assignedToUserId }: { id: number; assignedToUserId: number }) =>
      workAllocationApi.assign(id, assignedToUserId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['work-allocations'] });
    },
  });
};

export const useDeactivateWorkAllocation = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => workAllocationApi.deactivate(id),
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

export const useRecordSurveySubmission = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ taskProgressId, data }: { taskProgressId: number; data: RecordSurveyPayload }) =>
      taskProgressApi.recordSurveySubmission(taskProgressId, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['task-progress'] });
      queryClient.invalidateQueries({ queryKey: ['survey-details'] });
    },
  });
};

export const useSurveyDetails = (taskProgressId: number) => {
  return useQuery({
    queryKey: ['survey-details', taskProgressId],
    queryFn: async () => {
      const res = await surveyDetailApi.getByTaskProgressId(taskProgressId);
      return res.data ?? [];
    },
    enabled: !!taskProgressId,
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

export const useTaskAttachments = (taskProgressId: number) => {
  return useQuery({
    queryKey: ['task-attachments', taskProgressId],
    queryFn: async () => {
      const res = await taskAttachmentApi.getByTaskProgressId(taskProgressId);
      return res.data ?? [];
    },
    enabled: !!taskProgressId,
  });
};

export const useUploadTaskAttachment = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ taskProgressId, file }: { taskProgressId: number; file: File }) =>
      taskAttachmentApi.upload(taskProgressId, file),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['task-attachments'] });
    },
  });
};
