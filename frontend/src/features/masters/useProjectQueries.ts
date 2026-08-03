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
