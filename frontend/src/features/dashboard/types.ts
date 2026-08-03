export interface DashboardFilters {
  startDate?: string;
  endDate?: string;
  projectId?: number;
  divisionId?: number;
}

export interface AdminDashboardDto {
  totalRegisteredUsers: number;
  totalProjects: number;
  totalSurveysCompleted: number;
  totalSurveysPending: number;
  totalTicketsOpen: number;
  overallAttendancePercentage: number;
  overallSurveyCompletionPercentage: number;
}

export interface CoordinatorDashboardDto {
  teamSize: number;
  activeProjects: number;
  pendingTasks: number;
  completedSurveys: number;
  pendingSurveys: number;
  teamAttendancePercentage: number;
}

export type ExportFormat = 'pdf' | 'excel';

export interface DashboardExportCommand {
  format: ExportFormat;
  filters?: DashboardFilters;
}

export interface DashboardExportDto {
  fileUrl: string;
  fileName: string;
}

export interface FellowDashboardDto {
  totalAssignedProjects: number;
  completedSurveys: number;
  pendingSurveys: number;
  attendancePercentage: number;
  upcomingTraining: number;
  recentActivity: string;
}

export interface ProjectProgressDto {
  projectName: string;
  completionPercentage: number;
  totalSurveys: number;
  completedSurveys: number;
}

export interface RoleBasedDashboardDto {
  role: string;
  totalUsers: number;
  activeProjects: number;
  pendingApprovals: number;
  completedTasks: number;
}
