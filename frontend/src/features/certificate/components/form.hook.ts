import { useState, useCallback } from 'react';
import { useApplyForCertificate } from '../queries';
import { useAuth } from '../../auth';
import type { CertificateFormData } from '../types';

export function useCertificateForm() {
  const { user } = useAuth();
  const [formData, setFormData] = useState<CertificateFormData>({
    applicantId: 0,
    applicantName: '',
    programName: '',
    startDate: '',
    endDate: '',
    durationDays: 0,
  });

  const applyMutation = useApplyForCertificate();

  const updateField = useCallback(<K extends keyof CertificateFormData>(field: K, value: CertificateFormData[K]) => {
    setFormData((prev) => {
      const next = { ...prev, [field]: value };
      if (next.startDate && next.endDate) {
        const start = new Date(next.startDate);
        const end = new Date(next.endDate);
        const diffMs = end.getTime() - start.getTime();
        next.durationDays = diffMs > 0 ? Math.ceil(diffMs / (1000 * 60 * 60 * 24)) : 0;
      }
      return next;
    });
  }, []);

  const submit = useCallback(async () => {
    const payload: CertificateFormData = {
      ...formData,
      applicantName: user?.username ?? '',
    };
    await applyMutation.mutateAsync(payload);
  }, [formData, applyMutation, user]);

  return {
    formData,
    updateField,
    submit,
    isSubmitting: applyMutation.isPending,
  };
}
