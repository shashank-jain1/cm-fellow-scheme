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

export interface ReviewCycleDto {
  reviewCycleId: number;
  cycleName: string;
  startDate: string;
  endDate: string;
  isActive: boolean;
  createdOn: string;
}

export interface CreateReviewCycleCommand {
  cycleName: string;
  startDate: string;
  endDate: string;
}

export interface SelfAssessmentDto {
  selfAssessmentId: number;
  strengths: string;
  improvements: string;
  goalsAchieved: string;
  goalsMissed: string;
  trainingFeedback: string;
  overallRating: number;
  submittedOn: string;
}
