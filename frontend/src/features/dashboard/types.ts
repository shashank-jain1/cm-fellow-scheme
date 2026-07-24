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
