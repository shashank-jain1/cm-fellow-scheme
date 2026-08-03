import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { workAllocationApi } from './api';
import type { WorkAllocationFormData, UpdateProgressCommand, VerifyTaskCommand } from './types';

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

export const useDeactivateWorkAllocation = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => workAllocationApi.deactivate(id),
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

export const useUpdateProgress = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ workAllocationId, data }: { workAllocationId: number; data: UpdateProgressCommand }) =>
      workAllocationApi.updateProgress(workAllocationId, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['work-allocations'] });
      queryClient.invalidateQueries({ queryKey: ['task-progress'] });
    },
  });
};

export const useVerifyTask = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ workAllocationId, command }: { workAllocationId: number; command: VerifyTaskCommand }) =>
      workAllocationApi.verifyTask(workAllocationId, command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['work-allocations'] });
      queryClient.invalidateQueries({ queryKey: ['task-progress'] });
    },
  });
};

export const useCheckOverdueTasks = () => {
  return useQuery({
    queryKey: ['work-allocations', 'overdue'],
    queryFn: async () => {
      const res = await workAllocationApi.checkOverdue();
      return res.data ?? [];
    },
  });
};
