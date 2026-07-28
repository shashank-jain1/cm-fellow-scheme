import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { registrationApi } from './api';
import type { SubmitRegistrationPayload, VerifyOtpPayload } from './api';

export function useSubmitRegistrationMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: SubmitRegistrationPayload) => registrationApi.submitRegistration(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['registrations'] }),
  });
}

export function useVerifyMobileOtpMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ applicantId, data }: { applicantId: number; data: VerifyOtpPayload }) =>
      registrationApi.verifyMobileOtp(applicantId, data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['registrations'] }),
  });
}

export function useListRegistrationsQuery(params?: { searchTerm?: string; status?: string; pageNumber?: number; pageSize?: number }) {
  return useQuery({
    queryKey: ['registrations', params],
    queryFn: async () => {
      const res = await registrationApi.listRegistrations(params);
      return res.data ?? { items: [], totalCount: 0 };
    },
  });
}

export function useApproveRegistrationMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ applicantId, approvedBy }: { applicantId: number; approvedBy: number }) =>
      registrationApi.approveRegistration(applicantId, approvedBy),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['registrations'] }),
  });
}

export function useRejectRegistrationMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ applicantId, rejectedBy, reason }: { applicantId: number; rejectedBy: number; reason: string }) =>
      registrationApi.rejectRegistration(applicantId, rejectedBy, reason),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['registrations'] }),
  });
}

export function useRegistrationById(id: number) {
  return useQuery({
    queryKey: ['registrations', id],
    queryFn: async () => {
      const res = await registrationApi.getRegistrationById(id);
      return res.data;
    },
    enabled: !!id,
  });
}

export function useFellows() {
  return useQuery({
    queryKey: ['fellows'],
    queryFn: async () => {
      const res = await registrationApi.listRegistrations();
      return res.data?.items ?? [];
    },
  });
}

export function useFellow(id: number) {
  return useQuery({
    queryKey: ['fellows', id],
    queryFn: async () => {
      const res = await registrationApi.getRegistrationById(id);
      return res.data;
    },
    enabled: !!id,
  });
}

export function useForgotPasswordMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (email: string) => registrationApi.forgotPassword(email),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['registrations'] }),
  });
}

export function useResetPasswordMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ email, newPassword }: { email: string; newPassword: string }) =>
      registrationApi.resetPassword(email, newPassword),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['registrations'] }),
  });
}
