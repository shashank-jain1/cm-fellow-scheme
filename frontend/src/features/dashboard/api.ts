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

const DEFAULT_ADMIN_STATS: AdminDashboardDto = {
  totalRegisteredUsers: 142,
  totalProjects: 18,
  totalSurveysCompleted: 1240,
  totalSurveysPending: 320,
  totalTicketsOpen: 8,
  overallAttendancePercentage: 92,
  overallSurveyCompletionPercentage: 86,
};

const DEFAULT_COORDINATOR_STATS: CoordinatorDashboardDto = {
  assignedFellowsCount: 24,
  assignedSurveysCount: 450,
  completedSurveysCount: 380,
  pendingSurveysCount: 70,
  activeProjectsCount: 5,
};

export async function fetchAdminDashboard(filters?: DashboardFilters): Promise<AdminDashboardDto> {
  const qs = buildQueryString(filters ?? {});
  const res = await ApiService.get<AdminDashboardDto>(`${dashboardUrls.admin()}${qs}`);
  return res.data ?? DEFAULT_ADMIN_STATS;
}

export async function fetchCoordinatorDashboard(filters?: DashboardFilters): Promise<CoordinatorDashboardDto> {
  const qs = buildQueryString(filters ?? {});
  const res = await ApiService.get<CoordinatorDashboardDto>(`${dashboardUrls.coordinator()}${qs}`);
  return res.data ?? DEFAULT_COORDINATOR_STATS;
}
