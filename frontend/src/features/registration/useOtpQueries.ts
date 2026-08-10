import { useMutation, useQueryClient } from '@tanstack/react-query';
import { registrationApi } from './api';
import type { VerifyOtpPayload } from './api';

export function useSendOtpMutation() {
  return useMutation({
    mutationFn: (mobileNumber: string) => registrationApi.sendOtp(mobileNumber),
  });
}

export function useVerifyMobileOtpMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: VerifyOtpPayload) => registrationApi.verifyMobileOtp(data),
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
    mutationFn: ({ token, newPassword }: { token: string; newPassword: string }) =>
      registrationApi.resetPassword(token, newPassword),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['registrations'] }),
  });
}
