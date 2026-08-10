import ApiService from '../../services/ApiService';
import certificateUrls from './urls';
import type {
  CertificateApplicationDto,
  CertificateFormData,
  CertificateVerifyResult,
  ExitInterviewDto,
  ExitReadinessPayload,
  SubmitExitInterviewCommand,
  VerifyComplianceCommand,
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

/** Downloads through ApiService so the bearer token is sent; the endpoint requires auth. */
export async function downloadCertificate(certificateId: number, certificateNumber?: string): Promise<void> {
  const name = certificateNumber ? `certificate_${certificateNumber}.pdf` : `certificate_${certificateId}.pdf`;
  await ApiService.getBlob(certificateUrls.download(certificateId), name);
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

/** POST exit/compliance sets the exit record's clearance status; it returns no body. */
export async function verifyCompliance(command: VerifyComplianceCommand): Promise<void> {
  await ApiService.post(certificateUrls.exitCompliance(), command);
}

export async function getExitInterview(userAccountId: number): Promise<ExitInterviewDto | null> {
  const res = await ApiService.getOptional<ExitInterviewDto>(certificateUrls.exitInterviewByUser(userAccountId));
  return res.data ?? null;
}
