import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { workAllocationApi } from './api';
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
