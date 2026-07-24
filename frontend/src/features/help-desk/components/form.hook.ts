import { useState, useCallback } from 'react';
import { useRaiseTicket } from '../queries';
import type { TicketFormData } from '../types';

export function useTicketForm() {
  const [formData, setFormData] = useState<TicketFormData>({
    issueCategory: '',
    issueDescription: '',
    priority: 'Medium',
  });

  const raiseMutation = useRaiseTicket();

  const updateField = useCallback(<K extends keyof TicketFormData>(field: K, value: TicketFormData[K]) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
  }, []);

  const submit = useCallback(async () => {
    await raiseMutation.mutateAsync(formData);
    setFormData({ issueCategory: '', issueDescription: '', priority: 'Medium' });
  }, [formData, raiseMutation]);

  return {
    formData,
    updateField,
    submit,
    isSubmitting: raiseMutation.isPending,
    reset: () => setFormData({ issueCategory: '', issueDescription: '', priority: 'Medium' }),
  };
}
