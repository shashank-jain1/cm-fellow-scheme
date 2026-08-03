import { useMutation, useQueryClient } from '@tanstack/react-query';
import { registrationApi } from './api';
import type { VerifyOtpPayload } from './api';

export function useVerifyMobileOtpMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ applicantId, data }: { applicantId: number; data: VerifyOtpPayload }) =>
      registrationApi.verifyMobileOtp(applicantId, data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['registrations'] }),
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
