import { useState, useCallback } from 'react';
import { useCreateTrainingSchedule } from '../queries';
import type { ActivityFormData } from '../types';

const defaultFormData: ActivityFormData = {
  activityType: 'Training',
  projectId: 0,
  workProjectId: 0,
  date: '',
  startTime: '',
  endTime: '',
  mode: '',
  trainingTitle: '',
  trainingCategory: '',
  trainingDescription: '',
  trainerName: '',
  trainerMobile: '',
  attendanceRequired: false,
  meetingTitle: '',
  meetingAgenda: '',
  meetingDescription: '',
  conductPersonId: 0,
  coordinatorId: 0,
  momRequired: false,
  remarks: '',
};

export function useActivityForm() {
  const [formData, setFormData] = useState<ActivityFormData>({ ...defaultFormData });

  const createMutation = useCreateTrainingSchedule();

  const updateField = useCallback(<K extends keyof ActivityFormData>(field: K, value: ActivityFormData[K]) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
  }, []);

  const submit = useCallback(async () => {
    await createMutation.mutateAsync(formData);
    setFormData({ ...defaultFormData });
  }, [formData, createMutation]);

  return {
    formData,
    updateField,
    submit,
    isSubmitting: createMutation.isPending,
    reset: () => setFormData({ ...defaultFormData }),
  };
}
