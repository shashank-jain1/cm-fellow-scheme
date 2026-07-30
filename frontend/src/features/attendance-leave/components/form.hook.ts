import { useState } from 'react';
import type { ApplyLeaveCommand } from '../types';
import { useApplyLeave } from '../queries';
import { useAuth } from '../../auth';

interface LeaveFormData {
  leaveTypeId: number | null;
  fromDate: string;
  toDate: string;
  isHalfDay: boolean;
  reason: string;
  attachmentPath?: string;
}

interface FormErrors {
  leaveTypeId?: string;
  fromDate?: string;
  toDate?: string;
  reason?: string;
}

export function useApplyLeaveForm(onSuccess?: () => void) {
  const applyLeaveMutation = useApplyLeave();
  const { user } = useAuth();
  const [formData, setFormData] = useState<LeaveFormData>({
    leaveTypeId: null,
    fromDate: '',
    toDate: '',
    isHalfDay: false,
    reason: '',
    attachmentPath: undefined,
  });
  const [errors, setErrors] = useState<FormErrors>({});

  const validate = (): boolean => {
    const newErrors: FormErrors = {};

    if (!formData.leaveTypeId) newErrors.leaveTypeId = 'Leave type is required';
    if (!formData.fromDate) newErrors.fromDate = 'From date is required';
    if (!formData.toDate) newErrors.toDate = 'To date is required';
    if (formData.fromDate && formData.toDate && formData.fromDate > formData.toDate) {
      newErrors.toDate = 'To date must be after from date';
    }
    if (!formData.reason.trim()) newErrors.reason = 'Reason is required';

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleChange = (field: keyof LeaveFormData, value: unknown) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
    if (errors[field as keyof FormErrors]) {
      setErrors((prev) => ({ ...prev, [field]: undefined }));
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!validate()) return;
    if (!user) return;

    const command: ApplyLeaveCommand = {
      userAccountId: user.userAccountId,
      leaveTypeId: formData.leaveTypeId!,
      fromDate: formData.fromDate,
      toDate: formData.toDate,
      isHalfDay: formData.isHalfDay,
      reason: formData.reason,
      attachmentPath: formData.attachmentPath,
    };

    applyLeaveMutation.mutate(command, {
      onSuccess: () => {
        setFormData({
          leaveTypeId: null,
          fromDate: '',
          toDate: '',
          isHalfDay: false,
          reason: '',
          attachmentPath: undefined,
        });
        onSuccess?.();
      },
    });
  };

  return {
    formData,
    errors,
    handleChange,
    handleSubmit,
    isSubmitting: applyLeaveMutation.isPending,
  };
}
