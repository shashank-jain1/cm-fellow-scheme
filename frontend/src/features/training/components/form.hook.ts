import { useState, useCallback } from 'react';
import {
  useCreateTrainingSession,
  useCreateTrainingMeeting,
  useUploadTrainingMaterial,
  useUploadMeetingAttachment,
} from '../queries';
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
  const uploadMaterialMutation = useUploadTrainingMaterial();
  const uploadAttachmentMutation = useUploadMeetingAttachment();

  const updateField = useCallback(<K extends keyof ActivityFormData>(field: K, value: ActivityFormData[K]) => {
    setFormData((prev) => ({ ...prev, [field]: value }));
  }, []);

  const submit = useCallback(async () => {
    // The file pickers are part of the same form, but uploads are separate multipart
    // calls that need the new schedule's id — so they run once the activity exists.
    if (formData.activityType === 'Training') {
      const trainingScheduleId = await createSessionMutation.mutateAsync(formData);
      if (formData.trainingMaterialFile && trainingScheduleId) {
        await uploadMaterialMutation.mutateAsync({
          trainingScheduleId,
          file: formData.trainingMaterialFile,
        });
      }
    } else {
      const trainingScheduleId = await createMeetingMutation.mutateAsync(formData);
      if (formData.meetingAttachmentFile && trainingScheduleId) {
        await uploadAttachmentMutation.mutateAsync({
          trainingScheduleId,
          file: formData.meetingAttachmentFile,
        });
      }
    }
    setFormData({ ...defaultFormData });
  }, [formData, createSessionMutation, createMeetingMutation, uploadMaterialMutation, uploadAttachmentMutation]);

  return {
    formData,
    updateField,
    submit,
    isSubmitting:
      createSessionMutation.isPending ||
      createMeetingMutation.isPending ||
      uploadMaterialMutation.isPending ||
      uploadAttachmentMutation.isPending,
    reset: () => setFormData({ ...defaultFormData }),
  };
}
