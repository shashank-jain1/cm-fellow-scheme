import { useState, useCallback } from 'react';
import { useCreateTrainingSession, useCreateTrainingMeeting } from '../queries';
import type { ActivityFormData } from '../types';

const defaultFormData: ActivityFormData = {
  activityType: 'Training',
  projectId: 0,
  workProjectId: 0,
  date: '',
  startTime: '',
  endTime: '',
  mode: '',
  applicableDivisionIds: [],
  applicableDistrictIds: [],
  applicableBlockIds: [],
  remarks: '',
  trainingTitle: '',
  trainingCategory: '',
  trainingDescription: '',
  targetUserTypes: [],
  trainerName: '',
  trainerMobile: '',
  attendanceRequired: false,
  certificateRequired: false,
  trainingMaterialFile: null,
  meetingTitle: '',
  meetingAgenda: '',
  meetingDescription: '',
  conductPersonId: 0,
  coordinatorId: 0,
  participantIds: [],
  momRequired: false,
  meetingAttachmentFile: null,
};

export function useActivityForm() {
  const [formData, setFormData] = useState<ActivityFormData>({ ...defaultFormData });

  const createSessionMutation = useCreateTrainingSession();
  const createMeetingMutation = useCreateTrainingMeeting();

  const updateField = useCallback(<K extends keyof ActivityFormData>(field: K, value: ActivityFormData[K]) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
  }, []);

  const submit = useCallback(async () => {
    if (formData.activityType === 'Training') {
      await createSessionMutation.mutateAsync(formData);
    } else {
      await createMeetingMutation.mutateAsync(formData);
    }
    setFormData({ ...defaultFormData });
  }, [formData, createSessionMutation, createMeetingMutation]);

  return {
    formData,
    updateField,
    submit,
    isSubmitting: createSessionMutation.isPending || createMeetingMutation.isPending,
    reset: () => setFormData({ ...defaultFormData }),
  };
}
