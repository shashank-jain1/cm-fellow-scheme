import { useState } from 'react';
import type { LeaveApplicationFormData } from '../types';
import { useApplyLeave } from '../queries';

interface FormErrors {
  leaveType?: string;
  fromDate?: string;
  toDate?: string;
  leaveReason?: string;
}

export function useApplyLeaveForm(onSuccess?: () => void) {
  const applyLeaveMutation = useApplyLeave();
  const [formData, setFormData] = useState<LeaveApplicationFormData>({
    leaveType: '',
    fromDate: '',
    toDate: '',
    halfDayFullDay: false,
    leaveReason: '',
    attachmentFile: undefined,
  });
  const [errors, setErrors] = useState<FormErrors>({});

  const validate = (): boolean => {
    const newErrors: FormErrors = {};

    if (!formData.leaveType) newErrors.leaveType = 'Leave type is required';
    if (!formData.fromDate) newErrors.fromDate = 'From date is required';
    if (!formData.toDate) newErrors.toDate = 'To date is required';
    if (formData.fromDate && formData.toDate && formData.fromDate > formData.toDate) {
      newErrors.toDate = 'To date must be after from date';
    }
    if (!formData.leaveReason.trim()) newErrors.leaveReason = 'Reason is required';

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleChange = (field: keyof LeaveApplicationFormData, value: any) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
    if (errors[field as keyof FormErrors]) {
      setErrors((prev) => ({ ...prev, [field]: undefined }));
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!validate()) return;

    applyLeaveMutation.mutate(formData, {
      onSuccess: () => {
        setFormData({
          leaveType: '',
          fromDate: '',
          toDate: '',
          halfDayFullDay: false,
          leaveReason: '',
          attachmentFile: undefined,
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
