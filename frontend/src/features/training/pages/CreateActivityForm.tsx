import { useNavigate } from 'react-router-dom';
import { Button } from 'primereact/button';
import { useActivityForm } from '../components/form.hook';
import ActivityTypeSelector from '../components/ActivityTypeSelector';
import CommonActivityFields from '../components/CommonActivityFields';
import TrainingFieldsCard from '../components/TrainingFieldsCard';
import MeetingFieldsCard from '../components/MeetingFieldsCard';

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
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Schedule a new training or meeting activity
          </p>
        </div>
        <div style={{ display: 'flex', gap: 8 }}>
          <Button
            label="Cancel"
            icon="pi pi-times"
            className="btn btn-secondary"
            onClick={() => navigate('/training')}
          />
          <Button
            label="Save"
            icon="pi pi-check"
            className="btn btn-primary"
            loading={isSubmitting}
            onClick={handleSubmit}
          />
        </div>
      </div>

      <div className="glass-card" style={{ padding: 28 }}>
        <ActivityTypeSelector
          value={formData.activityType}
          onChange={(v) => updateField('activityType', v)}
        />

        <CommonActivityFields
          date={formData.date}
          startTime={formData.startTime}
          endTime={formData.endTime}
          mode={formData.mode}
          remarks={formData.remarks ?? ''}
          onDateChange={(v) => updateField('date', v)}
          onStartTimeChange={(v) => updateField('startTime', v)}
          onEndTimeChange={(v) => updateField('endTime', v)}
          onModeChange={(v) => updateField('mode', v)}
          onRemarksChange={(v) => updateField('remarks', v)}
        />

        {formData.activityType === 'Training' ? (
          <TrainingFieldsCard
            trainingTitle={formData.trainingTitle ?? ''}
            trainingCategory={formData.trainingCategory ?? ''}
            trainingDescription={formData.trainingDescription ?? ''}
            trainerName={formData.trainerName ?? ''}
            trainerMobile={formData.trainerMobile ?? ''}
            attendanceRequired={formData.attendanceRequired ?? false}
            onTrainingTitleChange={(v) => updateField('trainingTitle', v)}
            onTrainingCategoryChange={(v) => updateField('trainingCategory', v)}
            onTrainingDescriptionChange={(v) => updateField('trainingDescription', v)}
            onTrainerNameChange={(v) => updateField('trainerName', v)}
            onTrainerMobileChange={(v) => updateField('trainerMobile', v)}
            onAttendanceRequiredChange={(v) => updateField('attendanceRequired', v)}
          />
        ) : (
          <MeetingFieldsCard
            meetingTitle={formData.meetingTitle ?? ''}
            meetingAgenda={formData.meetingAgenda ?? ''}
            meetingDescription={formData.meetingDescription ?? ''}
            momRequired={formData.momRequired ?? false}
            onMeetingTitleChange={(v) => updateField('meetingTitle', v)}
            onMeetingAgendaChange={(v) => updateField('meetingAgenda', v)}
            onMeetingDescriptionChange={(v) => updateField('meetingDescription', v)}
            onMomRequiredChange={(v) => updateField('momRequired', v)}
          />
        )}
      </div>
    </div>
  );
}
