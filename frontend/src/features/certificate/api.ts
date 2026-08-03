import ApiService from '../../services/ApiService';
import certificateUrls from './urls';
import type {
  CertificateApplicationDto,
  CertificateFormData,
  CertificateVerifyResult,
  ComplianceCheckResult,
  ExitInterviewDto,
  ExitReadinessPayload,
  SubmitExitInterviewCommand,
} from './types';

export async function fetchCertificates(): Promise<CertificateApplicationDto[]> {
  const res = await ApiService.get<CertificateApplicationDto[]>(certificateUrls.list());
  return res.data ?? [];
}

export async function fetchCertificateDetail(id: number): Promise<CertificateApplicationDto> {
  const res = await ApiService.get<CertificateApplicationDto>(certificateUrls.detail(id));
  return res.data!;
}

export async function applyForCertificate(command: CertificateFormData): Promise<CertificateApplicationDto> {
  const res = await ApiService.post<CertificateApplicationDto>(certificateUrls.apply(), command);
  return res.data!;
}

export async function approveCertificate(id: number, verifiedBy: string): Promise<void> {
  await ApiService.put(certificateUrls.review(), {
    certificateId: id,
    status: 'Approved',
    verifiedBy,
  });
}

export async function rejectCertificate(id: number, verifiedBy: string): Promise<void> {
  await ApiService.put(certificateUrls.review(), {
    certificateId: id,
    status: 'Rejected',
    verifiedBy,
  });
}

export async function generateCertificate(certificateId: number): Promise<string> {
  const res = await ApiService.post<string>(certificateUrls.generate(certificateId), {});
  return res.data ?? '';
}

export function getDownloadUrl(id: number): string {
  const API_BASE = import.meta.env.VITE_API_URL || '/api/v1';
  return `${API_BASE}/${certificateUrls.download(id)}`;
}

export async function submitExitReadiness(payload: ExitReadinessPayload): Promise<void> {
  await ApiService.post(certificateUrls.exitReadiness(), payload);
}

export async function closeAndArchiveRecord(exitRecordId: number, approvedBy: number): Promise<void> {
  await ApiService.put(certificateUrls.exitCloseArchive(exitRecordId), { approvedBy });
}

export async function submitExitInterview(command: SubmitExitInterviewCommand): Promise<void> {
  await ApiService.post(certificateUrls.exitInterview(), command);
}

export async function generateCompletionCertificate(applicantId: number): Promise<string> {
  const res = await ApiService.post<string>(certificateUrls.generateCompletion(applicantId), {});
  return res.data ?? '';
}

export async function generateExperienceLetter(applicantId: number): Promise<string> {
  const res = await ApiService.post<string>(certificateUrls.generateExperience(applicantId), {});
  return res.data ?? '';
}

export async function verifyCertificate(certNumber: string): Promise<CertificateVerifyResult> {
  const res = await ApiService.get<CertificateVerifyResult>(certificateUrls.verify(certNumber));
  return res.data!;
}

export async function verifyCompliance(): Promise<ComplianceCheckResult> {
  const res = await ApiService.put<ComplianceCheckResult>(certificateUrls.exitCompliance(), {});
  return res.data!;
}

export async function getExitInterview(userAccountId: number): Promise<ExitInterviewDto | null> {
  const res = await ApiService.get<ExitInterviewDto | null>(certificateUrls.exitInterviewByUser(userAccountId));
  return res.data ?? null;
}
