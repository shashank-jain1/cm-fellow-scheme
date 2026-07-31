import { useQuery, useMutation } from '@tanstack/react-query';
import { fetchAdminDashboard, fetchCoordinatorDashboard, exportDashboardPdf, exportDashboardExcel } from './api';
import type { DashboardFilters } from './types';

export function useAdminDashboard(filters?: DashboardFilters) {
  return useQuery({
    queryKey: ['dashboard', 'admin', filters],
    queryFn: () => fetchAdminDashboard(filters),
  });
}

export function useCoordinatorDashboard(coordinatorId: number, filters?: DashboardFilters) {
  return useQuery({
    queryKey: ['dashboard', 'coordinator', coordinatorId, filters],
    queryFn: () => fetchCoordinatorDashboard(coordinatorId, filters),
    enabled: !!coordinatorId,
  });
}

export function useExportDashboardPdf() {
  return useMutation({
    mutationFn: (filters?: DashboardFilters) => exportDashboardPdf(filters),
  });
}

export function useExportDashboardExcel() {
  return useMutation({
    mutationFn: (filters?: DashboardFilters) => exportDashboardExcel(filters),
  });
}
