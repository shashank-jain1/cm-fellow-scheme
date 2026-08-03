import { useMutation, useQueryClient } from '@tanstack/react-query';
import { registrationApi } from './api';

export {
  useSubmitRegistrationMutation,
  useListRegistrationsQuery,
  useApproveRegistrationMutation,
  useRejectRegistrationMutation,
  useRegistrationById,
  useFellows,
  useFellow,
} from './useRegistrationQueries';

export {
  useVerifyMobileOtpMutation,
  useSendOtpMutation,
  useForgotPasswordMutation,
  useResetPasswordMutation,
} from './useOtpQueries';

export {
  useUpdateProfileMutation,
  useBulkApprovalMutation,
} from './useProfileQueries';

export function useBulkImportMutation() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (file: File) => registrationApi.bulkImportUsers(file),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['registrations'] }),
  });
}
