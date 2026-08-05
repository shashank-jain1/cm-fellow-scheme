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
  userAccountId: number;
  overallExperience: number;
  workEnvironment: number;
  learningOpportunities: number;
  teamCollaboration: number;
  improvementSuggestions: string;
  whatWorkedWell: string;
  wouldRecommend: boolean;
}

export interface ExitInterviewDto {
  exitInterviewId: number;
  userAccountId: number;
  overallExperience: number;
  workEnvironment: number;
  learningOpportunities: number;
  teamCollaboration: number;
  improvementSuggestions: string;
  whatWorkedWell: string;
  wouldRecommend: boolean;
  submittedOn: string;
}

export interface ComplianceCheckResult {
  isCompliant: boolean;
  flags: string;
}

export interface CertificateVerifyResult {
  isValid: boolean;
  certificateId: number;
  applicantName: string;
  programName: string;
  issuedOn: string;
  message: string;
}
