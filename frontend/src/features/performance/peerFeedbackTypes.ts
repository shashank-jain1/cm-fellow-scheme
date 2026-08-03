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
