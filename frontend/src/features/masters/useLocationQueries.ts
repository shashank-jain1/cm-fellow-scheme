import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { mastersApi } from './api';

export function useStates() {
  return useQuery({
    queryKey: ['masters', 'states'],
    queryFn: async () => {
      const res = await mastersApi.getStates();
      return res.data ?? [];
    },
  });
}

export function useDivisions(stateId?: number) {
  return useQuery({
    queryKey: ['masters', 'divisions', stateId ?? 'all'],
    queryFn: async () => {
      const res = await mastersApi.getDivisions(stateId);
      return res.data ?? [];
    },
  });
}

export function useDistricts(divisionId?: number) {
  return useQuery({
    queryKey: ['masters', 'districts', divisionId ?? 'all'],
    queryFn: async () => {
      const res = await mastersApi.getDistricts(divisionId);
      return res.data ?? [];
    },
  });
}

export function useBlocks(districtId?: number) {
  return useQuery({
    queryKey: ['masters', 'blocks', districtId ?? 'all'],
    queryFn: async () => {
      const res = await mastersApi.getBlocks(districtId);
      return res.data ?? [];
    },
  });
}

export function useGramPanchayats(blockId?: number) {
  return useQuery({
    queryKey: ['masters', 'gramPanchayats', blockId ?? 'all'],
    queryFn: async () => {
      const res = await mastersApi.getGramPanchayats(blockId);
      return res.data ?? [];
    },
  });
}
