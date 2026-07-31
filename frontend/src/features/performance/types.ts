export interface PerformanceSummaryDto {
  performanceEvaluationId: number;
  applicantName: string;
  projectName: string;
  totalSurveysAssigned: number;
  surveysCompleted: number;
  completionPercentage: number;
  performanceScore: number;
  performanceGrade: string;
  performanceStatus: string;
  evaluationRemarks?: string;
  reviewLevel?: string;
  reviewStatus?: string;
}

export interface RecordSupervisorRatingCommand {
  performanceEvaluationId: number;
  supervisorRating: number;
}

export interface RecordEvaluationRemarksCommand {
  performanceEvaluationId: number;
  evaluationRemarks: string;
}

export interface SubmitReviewRequest {
  action: string;
  performedBy: string;
  remarks?: string;
}

export interface ReviewHistoryDto {
  performanceReviewHistoryId: number;
  performanceEvaluationId: number;
  action: string;
  previousLevel: string;
  newLevel: string;
  previousStatus: string;
  newStatus: string;
  performedBy?: string;
  remarks?: string;
  performedOn: string;
}

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

export interface PeerFeedbackDto {
  peerFeedbackId: number;
  performanceEvaluationId: number;
  technicalRating: number;
  communicationRating: number;
  teamworkRating: number;
  leadershipRating: number;
  overallRating: number;
  comments: string;
  isAnonymous: boolean;
  submittedBy?: string;
  createdOn: string;
}

export interface SubmitPeerFeedbackCommand {
  performanceEvaluationId: number;
  technicalRating: number;
  communicationRating: number;
  teamworkRating: number;
  leadershipRating: number;
  overallRating: number;
  comments: string;
  isAnonymous: boolean;
}

export interface SubmitSelfAssessmentCommand {
  strengths: string;
  improvements: string;
  goalsAchieved: string;
  goalsMissed: string;
  trainingFeedback: string;
  overallRating: number;
}
