import TrainingFieldsCard from '../components/TrainingFieldsCard';
import MeetingFieldsCard from '../components/MeetingFieldsCard';

interface ActivityDetailsFieldsProps {
  activityType: string;
  formData: Record<string, unknown>;
  updateField: (field: any, value: any) => void;
}

export default function ActivityDetailsFields({
  activityType,
  formData,
  updateField,
}: ActivityDetailsFieldsProps) {
  if (activityType === 'Training') {
    return (
      <TrainingFieldsCard
        trainingTitle={(formData.trainingTitle as string) ?? ''}
        trainingCategory={(formData.trainingCategory as string) ?? ''}
        trainingDescription={(formData.trainingDescription as string) ?? ''}
        targetUserTypes={(formData.targetUserTypes as string[]) ?? []}
        trainerName={(formData.trainerName as string) ?? ''}
        trainerMobile={(formData.trainerMobile as string) ?? ''}
        attendanceRequired={(formData.attendanceRequired as boolean) ?? false}
        certificateRequired={(formData.certificateRequired as boolean) ?? false}
        trainingMaterialFile={(formData.trainingMaterialFile as File | null) ?? null}
        onTrainingTitleChange={(v) => updateField('trainingTitle', v)}
        onTrainingCategoryChange={(v) => updateField('trainingCategory', v)}
        onTrainingDescriptionChange={(v) => updateField('trainingDescription', v)}
        onTargetUserTypesChange={(v) => updateField('targetUserTypes', v)}
        onTrainerNameChange={(v) => updateField('trainerName', v)}
        onTrainerMobileChange={(v) => updateField('trainerMobile', v)}
        onAttendanceRequiredChange={(v) => updateField('attendanceRequired', v)}
        onCertificateRequiredChange={(v) => updateField('certificateRequired', v)}
        onTrainingMaterialChange={(v) => updateField('trainingMaterialFile', v)}
      />
    );
  }

  return (
    <MeetingFieldsCard
      meetingTitle={(formData.meetingTitle as string) ?? ''}
      meetingAgenda={(formData.meetingAgenda as string) ?? ''}
      meetingDescription={(formData.meetingDescription as string) ?? ''}
      conductPersonId={(formData.conductPersonId as number) ?? 0}
      coordinatorId={(formData.coordinatorId as number) ?? 0}
      participantIds={formData.participantIds as number[]}
      momRequired={(formData.momRequired as boolean) ?? false}
      meetingAttachmentFile={(formData.meetingAttachmentFile as File | null) ?? null}
      onMeetingTitleChange={(v) => updateField('meetingTitle', v)}
      onMeetingAgendaChange={(v) => updateField('meetingAgenda', v)}
      onMeetingDescriptionChange={(v) => updateField('meetingDescription', v)}
      onConductPersonChange={(v) => updateField('conductPersonId', v)}
      onCoordinatorChange={(v) => updateField('coordinatorId', v)}
      onParticipantsChange={(v) => updateField('participantIds', v)}
      onMomRequiredChange={(v) => updateField('momRequired', v)}
      onMeetingAttachmentChange={(v) => updateField('meetingAttachmentFile', v)}
    />
  );
}
