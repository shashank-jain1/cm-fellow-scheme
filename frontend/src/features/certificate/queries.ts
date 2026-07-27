import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { fetchCertificates, fetchCertificateDetail, applyForCertificate, approveCertificate, rejectCertificate, submitExitReadiness } from './api';
import type { CertificateFormData } from './types';
import type { ExitReadinessPayload } from './api';

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
    mutationFn: (id: number) => approveCertificate(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['certificates'] });
    },
  });
}

export function useRejectCertificate() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => rejectCertificate(id),
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
