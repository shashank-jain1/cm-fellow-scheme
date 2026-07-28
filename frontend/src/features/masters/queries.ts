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

export function useProjects() {
  return useQuery({
    queryKey: ['masters', 'projects'],
    queryFn: async () => {
      const res = await mastersApi.getProjects();
      return res.data ?? [];
    },
  });
}

export function useWorks(projectId?: number) {
  return useQuery({
    queryKey: ['masters', 'works', projectId ?? 'all'],
    queryFn: async () => {
      const res = await mastersApi.getWorks(projectId);
      return res.data ?? [];
    },
  });
}

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

export function useCreateProject() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: mastersApi.createProject,
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'projects'] }),
  });
}

export function useCreateWork() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: mastersApi.createWork,
    onSuccess: () => qc.invalidateQueries({ queryKey: ['masters', 'works'] }),
  });
}

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
