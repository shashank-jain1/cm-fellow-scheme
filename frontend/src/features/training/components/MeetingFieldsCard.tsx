import { InputText } from 'primereact/inputtext';
import { InputTextarea } from 'primereact/inputtextarea';
import { InputSwitch } from 'primereact/inputswitch';
import FormSelect from '../../../shared/components/FormSelect';
import { useLookupOptions } from '../../../shared/hooks/useMasters';

interface Props {
  meetingTitle: string;
  meetingAgenda: string;
  meetingDescription: string;
  momRequired: boolean;
  onMeetingTitleChange: (v: string) => void;
  onMeetingAgendaChange: (v: string) => void;
  onMeetingDescriptionChange: (v: string) => void;
  onMomRequiredChange: (v: boolean) => void;
}

export default function MeetingFieldsCard({
  meetingTitle,
  meetingAgenda,
  meetingDescription,
  momRequired,
  onMeetingTitleChange,
  onMeetingAgendaChange,
  onMeetingDescriptionChange,
  onMomRequiredChange,
}: Props) {
  const agendaOptions = useLookupOptions('MeetingAgenda');
  return (
    <div className="glass-card" style={{ padding: 24, marginTop: 20 }}>
      <h3 className="form-section-header">Meeting Details</h3>
      <div className="form-grid">
        <div className="form-field">
          <label>Meeting Title</label>
          <InputText
            value={meetingTitle}
            onChange={(e) => onMeetingTitleChange(e.target.value)}
            placeholder="Enter meeting title"
          />
        </div>
        <div className="form-field">
          <label>Agenda</label>
          <FormSelect
            value={meetingAgenda}
            options={agendaOptions}
            onChange={(val) => onMeetingAgendaChange(val)}
            placeholder="Select Agenda"
          />
        </div>
        <div className="form-field full-width">
          <label>Description</label>
          <InputTextarea
            value={meetingDescription}
            onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onMeetingDescriptionChange(e.target.value)}
            rows={3}
          />
        </div>
        <div className="form-field">
          <label>MOM Required</label>
          <div style={{ display: 'flex', alignItems: 'center', gap: 10, height: 40 }}>
            <InputSwitch
              checked={momRequired}
              onChange={(e) => onMomRequiredChange(e.value ?? false)}
            />
            <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>
              {momRequired ? 'Yes' : 'No'}
            </span>
          </div>
        </div>
      </div>
    </div>
  );
}
