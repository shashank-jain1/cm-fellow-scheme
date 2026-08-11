export type GoalStatus = 'NotStarted' | 'InProgress' | 'Completed' | 'Missed';

export interface PerformanceGoalDto {
  performanceGoalId: number;
  userAccountId: number;
  goalTitle: string;
  description: string | null;
  targetDate: string;
  status: GoalStatus;
  reviewCycleId: number | null;
  createdOn: string;
}

export interface CreateGoalCommand {
  title: string;
  description: string;
  targetDate: string;
}

export interface UpdateGoalStatusCommand {
  status: GoalStatus;
}

export type PipStatus = 'NotStarted' | 'InProgress' | 'Completed' | 'Cancelled';

export interface ImprovementPlanDto {
  improvementPlanId: number;
  userAccountId: number;
  planTitle: string;
  description: string | null;
  startDate: string;
  endDate: string;
  status: PipStatus;
  createdBy: number;
  createdOn: string;
}

export interface CreateImprovementPlanCommand {
  title: string;
  description: string;
  startDate: string;
  endDate: string;
}
