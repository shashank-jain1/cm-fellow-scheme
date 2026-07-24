import { useQuery } from '@tanstack/react-query';
import { fetchAdminDashboard, fetchCoordinatorDashboard } from './api';
import type { DashboardFilters } from './types';

export function useAdminDashboard(filters?: DashboardFilters) {
  return useQuery({
    queryKey: ['dashboard', 'admin', filters],
    queryFn: () => fetchAdminDashboard(filters),
  });
}

export function useCoordinatorDashboard(filters?: DashboardFilters) {
  return useQuery({
    queryKey: ['dashboard', 'coordinator', filters],
    queryFn: () => fetchCoordinatorDashboard(filters),
  });
}
