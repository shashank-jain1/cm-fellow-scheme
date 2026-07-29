import { useNavigate } from 'react-router-dom';
import { useActivityForm } from '../components/form.hook';
import ActivityTypeSelector from '../components/ActivityTypeSelector';
import CommonActivityFields from '../components/CommonActivityFields';
import TrainingFieldsCard from '../components/TrainingFieldsCard';
import MeetingFieldsCard from '../components/MeetingFieldsCard';
import { AppButton } from '../../../shared/components/ui';

export default function CreateActivityForm() {
  const navigate = useNavigate();
  const { formData, updateField, submit, isSubmitting } = useActivityForm();

  const handleSubmit = async () => {
    await submit();
    navigate('/training');
  };

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Create Activity</h1>
          <p style={{ color: 'var(--text-muted)', marginTop: 4 }}>
            Schedule a new training or meeting activity
          </p>
        </div>
        <div style={{ display: 'flex', gap: 8 }}>
          <AppButton variant="secondary" icon="pi pi-times" onClick={() => navigate('/training')}>
            Cancel
          </AppButton>
          <AppButton icon="pi pi-check" loading={isSubmitting} onClick={handleSubmit}>
            Save
          </AppButton>
        </div>
      </div>

      <div className="card" style={{ padding: 28 }}>
        <ActivityTypeSelector
          value={formData.activityType}
          onChange={(v) => updateField('activityType', v)}
        />

        <CommonActivityFields
          projectId={formData.projectId}
          workProjectId={formData.workProjectId}
          date={formData.date}
          startTime={formData.startTime}
          endTime={formData.endTime}
          mode={formData.mode}
          applicableDivisionIds={formData.applicableDivisionIds}
          applicableDistrictIds={formData.applicableDistrictIds}
          applicableBlockIds={formData.applicableBlockIds}
          remarks={formData.remarks ?? ''}
          onProjectChange={(v) => updateField('projectId', v)}
          onWorkChange={(v) => updateField('workProjectId', v)}
          onDateChange={(v) => updateField('date', v)}
          onStartTimeChange={(v) => updateField('startTime', v)}
          onEndTimeChange={(v) => updateField('endTime', v)}
          onModeChange={(v) => updateField('mode', v)}
          onDivisionsChange={(v) => updateField('applicableDivisionIds', v)}
          onDistrictsChange={(v) => updateField('applicableDistrictIds', v)}
          onBlocksChange={(v) => updateField('applicableBlockIds', v)}
          onRemarksChange={(v) => updateField('remarks', v)}
        />

        {formData.activityType === 'Training' ? (
          <TrainingFieldsCard
            trainingTitle={formData.trainingTitle ?? ''}
            trainingCategory={formData.trainingCategory ?? ''}
            trainingDescription={formData.trainingDescription ?? ''}
            targetUserTypes={formData.targetUserTypes}
            trainerName={formData.trainerName ?? ''}
            trainerMobile={formData.trainerMobile ?? ''}
            attendanceRequired={formData.attendanceRequired ?? false}
            certificateRequired={formData.certificateRequired ?? false}
            trainingMaterialFile={formData.trainingMaterialFile ?? null}
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
        ) : (
          <MeetingFieldsCard
            meetingTitle={formData.meetingTitle ?? ''}
            meetingAgenda={formData.meetingAgenda ?? ''}
            meetingDescription={formData.meetingDescription ?? ''}
            conductPersonId={formData.conductPersonId ?? 0}
            coordinatorId={formData.coordinatorId ?? 0}
            participantIds={formData.participantIds}
            momRequired={formData.momRequired ?? false}
            meetingAttachmentFile={formData.meetingAttachmentFile ?? null}
            onMeetingTitleChange={(v) => updateField('meetingTitle', v)}
            onMeetingAgendaChange={(v) => updateField('meetingAgenda', v)}
            onMeetingDescriptionChange={(v) => updateField('meetingDescription', v)}
            onConductPersonChange={(v) => updateField('conductPersonId', v)}
            onCoordinatorChange={(v) => updateField('coordinatorId', v)}
            onParticipantsChange={(v) => updateField('participantIds', v)}
            onMomRequiredChange={(v) => updateField('momRequired', v)}
            onMeetingAttachmentChange={(v) => updateField('meetingAttachmentFile', v)}
          />
        )}
      </div>
    </div>
  );
}
