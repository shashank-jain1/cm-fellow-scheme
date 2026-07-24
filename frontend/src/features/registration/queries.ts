import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { registrationApi } from './api';

export function useFellows() {
  return useQuery({
    queryKey: ['fellows'],
    queryFn: async () => {
      const res = await registrationApi.getFellows();
      return res.data ?? [];
    },
  });
}

export function useFellow(id: number) {
  return useQuery({
    queryKey: ['fellows', id],
    queryFn: async () => {
      const res = await registrationApi.getFellow(id);
      return res.data;
    },
    enabled: !!id,
  });
}

export function useCreateFellow() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: Parameters<typeof registrationApi.createFellow>[0]) =>
      registrationApi.createFellow(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['fellows'] }),
  });
}

export function useUpdateFellow() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: number; data: Parameters<typeof registrationApi.updateFellow>[1] }) =>
      registrationApi.updateFellow(id, data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['fellows'] }),
  });
}

export function useDeleteFellow() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => registrationApi.deleteFellow(id),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['fellows'] }),
  });
}
