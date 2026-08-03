import { useQuery, useMutation } from '@tanstack/react-query';
import { fetchAdminDashboard, fetchCoordinatorDashboard, fetchFellowDashboard, fetchDashboardByRole, fetchProjectProgress, exportDashboardPdf, exportDashboardExcel } from './api';
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

export function useFellowDashboard(userId: number) {
  return useQuery({
    queryKey: ['dashboard', 'fellow', userId],
    queryFn: () => fetchFellowDashboard(userId),
    enabled: !!userId,
  });
}

export function useDashboardByRole(role: string) {
  return useQuery({
    queryKey: ['dashboard', 'role', role],
    queryFn: () => fetchDashboardByRole(role),
    enabled: !!role,
  });
}

export function useProjectProgress() {
  return useQuery({
    queryKey: ['dashboard', 'project-progress'],
    queryFn: () => fetchProjectProgress(),
  });
}
