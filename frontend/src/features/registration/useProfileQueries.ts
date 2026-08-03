import { useMutation, useQueryClient } from '@tanstack/react-query';
import { registrationApi } from './api';
import type { ProfileUpdatePayload, BulkApprovalPayload } from './types';

export function useUpdateProfileMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: ({ applicantId, data }: { applicantId: number; data: ProfileUpdatePayload }) =>
      registrationApi.updateProfile(applicantId, data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['registrations'] }),
  });
}

export function useBulkApprovalMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (data: BulkApprovalPayload) => registrationApi.bulkApprove(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['registrations'] }),
  });
}
