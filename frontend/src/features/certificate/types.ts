export interface CertificateFormData {
  applicantId: number;
  programName: string;
  startDate: string;
  endDate: string;
}

export interface CertificateApplicationDto {
  certificateId: number;
  applicantName: string;
  programName: string;
  startDate: string;
  endDate: string;
  durationDays: number;
  status: string;
  certificatePdfPath?: string;
}
