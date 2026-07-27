import ApiService from '../../services/ApiService';
import certificateUrls from './urls';
import type { CertificateApplicationDto, CertificateFormData } from './types';

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

export async function approveCertificate(id: number): Promise<void> {
  await ApiService.put(certificateUrls.review(), { certificateId: id, ReviewStatus: 'approved' });
}

export async function rejectCertificate(id: number): Promise<void> {
  await ApiService.put(certificateUrls.review(), { certificateId: id, ReviewStatus: 'rejected' });
}

export async function downloadCertificate(id: number): Promise<void> {
  const API_BASE = import.meta.env.VITE_API_URL || '/api/v1';
  window.open(`${API_BASE}/${certificateUrls.download(id)}`, '_blank');
}
