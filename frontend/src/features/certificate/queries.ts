import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import {
  fetchCertificates,
  fetchCertificateDetail,
  applyForCertificate,
  approveCertificate,
  rejectCertificate,
  submitExitReadiness,
  generateCertificate,
  closeAndArchiveRecord,
  submitExitInterview,
  generateCompletionCertificate,
  generateExperienceLetter,
  verifyCertificate,
  verifyCompliance,
  getExitInterview,
} from './api';
import type { CertificateFormData, ExitReadinessPayload, SubmitExitInterviewCommand, VerifyComplianceCommand } from './types';

export function useCertificates() {
  return useQuery({
    queryKey: ['certificates'],
    queryFn: fetchCertificates,
  });
}

export function useCertificateDetail(id: number) {
  return useQuery({
    queryKey: ['certificate', id],
    queryFn: () => fetchCertificateDetail(id),
    enabled: !!id,
  });
}

export function useApplyForCertificate() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: CertificateFormData) => applyForCertificate(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['certificates'] });
    },
  });
}

export function useApproveCertificate() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, verifiedBy }: { id: number; verifiedBy: string }) => approveCertificate(id, verifiedBy),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['certificates'] });
    },
  });
}

export function useRejectCertificate() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, verifiedBy }: { id: number; verifiedBy: string }) => rejectCertificate(id, verifiedBy),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['certificates'] });
    },
  });
}

export function useGenerateCertificate() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (certificateId: number) => generateCertificate(certificateId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['certificates'] });
    },
  });
}

export function useSubmitExitReadiness() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (payload: ExitReadinessPayload) => submitExitReadiness(payload),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['certificates'] });
    },
  });
}

export function useCloseAndArchiveRecord() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ exitRecordId, approvedBy }: { exitRecordId: number; approvedBy: number }) =>
      closeAndArchiveRecord(exitRecordId, approvedBy),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['certificates'] });
    },
  });
}

export function useSubmitExitInterview() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (command: SubmitExitInterviewCommand) => submitExitInterview(command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['exit-interview'] });
    },
  });
}

export function useGenerateCompletionCertificate() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (applicantId: number) => generateCompletionCertificate(applicantId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['certificates'] });
    },
  });
}

export function useGenerateExperienceLetter() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (applicantId: number) => generateExperienceLetter(applicantId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['certificates'] });
    },
  });
}

export function useVerifyCertificate() {
  return useMutation({
    mutationFn: (certNumber: string) => verifyCertificate(certNumber),
  });
}

export function useVerifyCompliance() {
  return useMutation({
    mutationFn: (command: VerifyComplianceCommand) => verifyCompliance(command),
  });
}

export function useExitInterview(userAccountId: number) {
  return useQuery({
    queryKey: ['exit-interview', userAccountId],
    queryFn: () => getExitInterview(userAccountId),
    enabled: !!userAccountId,
  });
}
