import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { moduleAccessApi } from './api';
import type { BulkUpdateRequest, GrantAccessRequest } from './types';

export function useAuditLogQuery(params?: { userAccountId?: number; moduleMasterId?: number; pageNumber?: number; pageSize?: number }) {
  return useQuery({
    queryKey: ['audit-logs', params],
    queryFn: async () => {
      const res = await moduleAccessApi.fetchAuditLogs({ ...params, pageSize: params?.pageSize ?? 50, pageNumber: params?.pageNumber ?? 1 });
      return res.data ?? [];
    },
  });
}

export function useAllModuleAccessQuery() {
  return useQuery({
    queryKey: ['moduleAccess', 'all'],
    queryFn: async () => {
      const res = await moduleAccessApi.fetchAllModuleAccess();
      return res.data ?? [];
    },
  });
}

export function useUserModuleAccessQuery(userAccountId: number) {
  return useQuery({
    queryKey: ['moduleAccess', 'user', userAccountId],
    queryFn: async () => {
      const res = await moduleAccessApi.fetchUserModuleAccess(userAccountId);
      return res.data ?? [];
    },
    enabled: userAccountId > 0,
  });
}

export function useBulkUpdateMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: BulkUpdateRequest) => moduleAccessApi.bulkUpdateModuleAccess(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['moduleAccess'] }),
  });
}

export function useGrantAccessMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: GrantAccessRequest) => moduleAccessApi.grantModuleAccess(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['moduleAccess'] }),
  });
}

export function useRevokeAccessMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => moduleAccessApi.revokeModuleAccess(id),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['moduleAccess'] }),
  });
}

export function useModulesQuery() {
  return useQuery({
    queryKey: ['moduleAccess', 'modules'],
    queryFn: async () => {
      const res = await moduleAccessApi.fetchModules();
      return res.data ?? [];
    },
  });
}
