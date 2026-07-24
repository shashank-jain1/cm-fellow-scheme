import { RadioButton } from 'primereact/radiobutton';

interface Props {
  value: 'Training' | 'Meeting';
  onChange: (value: 'Training' | 'Meeting') => void;
}

export default function ActivityTypeSelector({ value, onChange }: Props) {
  return (
    <div style={{ display: 'flex', gap: 24, marginBottom: 16 }}>
      <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
        <RadioButton
          inputId="activityTraining"
          name="activityType"
          value="Training"
          checked={value === 'Training'}
          onChange={() => onChange('Training')}
        />
        <label htmlFor="activityTraining">Training</label>
      </div>
      <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
        <RadioButton
          inputId="activityMeeting"
          name="activityType"
          value="Meeting"
          checked={value === 'Meeting'}
          onChange={() => onChange('Meeting')}
        />
        <label htmlFor="activityMeeting">Meeting</label>
      </div>
    </div>
  );
}
