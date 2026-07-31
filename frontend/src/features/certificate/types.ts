export interface CertificateFormData {
  applicantId: number;
  applicantName: string;
  programName: string;
  startDate: string;
  endDate: string;
  durationDays: number;
}

export interface CertificateApplicationDto {
  certificateId: number;
  applicantId: number;
  applicantName: string;
  programName: string;
  startDate: string;
  endDate: string;
  durationDays: number;
  verifiedBy?: string;
  certificateIssueDate?: string;
  certificatePdfPath?: string;
  status: string;
  createdOn: string;
}

export interface ExitReadinessPayload {
  applicantId: number;
  completionStatus: string;
  verificationFlags: string;
  createdBy: string;
}

export interface SubmitExitInterviewCommand {
  overallExperience: number;
  workEnvironment: number;
  learningOpportunities: number;
  teamCollaboration: number;
  improvementSuggestions: string;
  whatWorkedWell: string;
  wouldRecommend: boolean;
}
