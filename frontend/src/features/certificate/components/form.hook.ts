import { useState, useCallback } from 'react';
import { useApplyForCertificate } from '../queries';
import type { CertificateFormData } from '../types';

export function useCertificateForm() {
  const [formData, setFormData] = useState<CertificateFormData>({
    applicantId: 0,
    programName: '',
    startDate: '',
    endDate: '',
  });

  const applyMutation = useApplyForCertificate();

  const updateField = useCallback(<K extends keyof CertificateFormData>(field: K, value: CertificateFormData[K]) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
  }, []);

  const submit = useCallback(async () => {
    await applyMutation.mutateAsync(formData);
  }, [formData, applyMutation]);

  return {
    formData,
    updateField,
    submit,
    isSubmitting: applyMutation.isPending,
  };
}
