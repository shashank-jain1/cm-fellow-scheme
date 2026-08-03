import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { mastersApi } from './api';

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
