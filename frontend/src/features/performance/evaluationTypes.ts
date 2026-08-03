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

export interface SubmitSelfAssessmentCommand {
  strengths: string;
  improvements: string;
  goalsAchieved: string;
  goalsMissed: string;
  trainingFeedback: string;
  overallRating: number;
}
