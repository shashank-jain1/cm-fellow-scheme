import ApiService from '../../services/ApiService';
import dashboardUrls from './urls';
import type { AdminDashboardDto, CoordinatorDashboardDto, DashboardFilters } from './types';

function buildQueryString(filters: DashboardFilters): string {
  const params = new URLSearchParams();
  if (filters.startDate) params.append('startDate', filters.startDate);
  if (filters.endDate) params.append('endDate', filters.endDate);
  if (filters.projectId) params.append('projectId', String(filters.projectId));
  if (filters.divisionId) params.append('divisionId', String(filters.divisionId));
  const qs = params.toString();
  return qs ? `?${qs}` : '';
}

export async function fetchAdminDashboard(filters?: DashboardFilters): Promise<AdminDashboardDto> {
  const qs = buildQueryString(filters ?? {});
  const res = await ApiService.get<AdminDashboardDto>(`${dashboardUrls.admin()}${qs}`);
  return res.data!;
}

export async function fetchCoordinatorDashboard(coordinatorId: number, filters?: DashboardFilters): Promise<CoordinatorDashboardDto> {
  const qs = buildQueryString(filters ?? {});
  const res = await ApiService.get<CoordinatorDashboardDto>(`${dashboardUrls.coordinator(coordinatorId)}${qs}`);
  return res.data!;
}
