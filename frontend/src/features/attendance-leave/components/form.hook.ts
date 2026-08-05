import { useState } from 'react';
import type { ApplyLeaveCommand } from '../types';
import { useApplyLeave } from '../queries';
import { useAuth } from '../../auth';

interface LeaveFormData {
  leaveType: string;
  fromDate: string;
  toDate: string;
  halfDayFullDay: string;
  leaveReason: string;
  attachmentPath?: string;
}

interface FormErrors {
  leaveType?: string;
  fromDate?: string;
  toDate?: string;
  leaveReason?: string;
}

export function useApplyLeaveForm(onSuccess?: () => void) {
  const applyLeaveMutation = useApplyLeave();
  const { user } = useAuth();
  const [formData, setFormData] = useState<LeaveFormData>({
    leaveType: '',
    fromDate: '',
    toDate: '',
    halfDayFullDay: 'Full',
    leaveReason: '',
    attachmentPath: undefined,
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

    const from = new Date(formData.fromDate);
    const to = new Date(formData.toDate);
    const diffTime = Math.abs(to.getTime() - from.getTime());
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24)) + 1;
    const numberOfDays = formData.halfDayFullDay === 'Half' ? 0.5 : diffDays;

    const command: ApplyLeaveCommand = {
      applicantId: user.userAccountId,
      leaveType: formData.leaveType,
      fromDate: formData.fromDate,
      toDate: formData.toDate,
      numberOfDays,
      halfDayFullDay: formData.halfDayFullDay,
      leaveReason: formData.leaveReason,
      attachmentPath: formData.attachmentPath,
      reportingManagerName: '',
      createdBy: user.username,
    };

    applyLeaveMutation.mutate(command, {
      onSuccess: () => {
        setFormData({
          leaveType: '',
          fromDate: '',
          toDate: '',
          halfDayFullDay: 'Full',
          leaveReason: '',
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
