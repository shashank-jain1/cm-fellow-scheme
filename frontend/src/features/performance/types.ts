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
}

export interface RecordSupervisorRatingCommand {
  performanceEvaluationId: number;
  supervisorRating: number;
}

export interface RecordEvaluationRemarksCommand {
  performanceEvaluationId: number;
  evaluationRemarks: string;
}
