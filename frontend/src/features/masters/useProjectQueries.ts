import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { mastersApi } from './api';

export function useCreateState() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: mastersApi.createState,
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'states'] }),
  });
}

export function useCreateDivision() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: mastersApi.createDivision,
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'divisions'] }),
  });
}

export function useCreateDistrict() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: mastersApi.createDistrict,
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'districts'] }),
  });
}

export function useCreateBlock() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: mastersApi.createBlock,
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'blocks'] }),
  });
}

export function useCreateGramPanchayat() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: mastersApi.createGramPanchayat,
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'gramPanchayats'] }),
  });
}

export function useUpdateState() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: number; data: Parameters<typeof mastersApi.updateState>[1] }) => mastersApi.updateState(id, data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'states'] }),
  });
}

export function useDeleteState() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => mastersApi.deleteState(id),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'states'] }),
  });
}

export function useUpdateDivision() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: number; data: Parameters<typeof mastersApi.updateDivision>[1] }) => mastersApi.updateDivision(id, data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'divisions'] }),
  });
}

export function useDeleteDivision() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => mastersApi.deleteDivision(id),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'divisions'] }),
  });
}

export function useUpdateDistrict() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: number; data: Parameters<typeof mastersApi.updateDistrict>[1] }) => mastersApi.updateDistrict(id, data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'districts'] }),
  });
}

export function useDeleteDistrict() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => mastersApi.deleteDistrict(id),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'districts'] }),
  });
}

export function useUpdateBlock() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: number; data: Parameters<typeof mastersApi.updateBlock>[1] }) => mastersApi.updateBlock(id, data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'blocks'] }),
  });
}

export function useDeleteBlock() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => mastersApi.deleteBlock(id),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'blocks'] }),
  });
}

export function useUpdateGramPanchayat() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: number; data: Parameters<typeof mastersApi.updateGramPanchayat>[1] }) => mastersApi.updateGramPanchayat(id, data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'gramPanchayats'] }),
  });
}

export function useDeleteGramPanchayat() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => mastersApi.deleteGramPanchayat(id),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'gramPanchayats'] }),
  });
}
