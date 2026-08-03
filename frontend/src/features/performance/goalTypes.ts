export type GoalStatus = 'NotStarted' | 'InProgress' | 'Completed' | 'Missed';

export interface PerformanceGoalDto {
  goalId: number;
  userId: number;
  title: string;
  description: string;
  targetDate: string;
  status: GoalStatus;
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
  userId: number;
  title: string;
  description: string;
  startDate: string;
  endDate: string;
  status: PipStatus;
  createdOn: string;
}

export interface CreateImprovementPlanCommand {
  title: string;
  description: string;
  startDate: string;
  endDate: string;
}
