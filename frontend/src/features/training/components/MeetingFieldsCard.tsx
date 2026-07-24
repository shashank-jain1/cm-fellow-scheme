import { InputText } from 'primereact/inputtext';
import { InputTextarea } from 'primereact/inputtextarea';
import { Dropdown } from 'primereact/dropdown';
import { InputSwitch } from 'primereact/inputswitch';

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

const agendaOptions = [
  { label: 'Review', value: 'Review' },
  { label: 'Planning', value: 'Planning' },
  { label: 'Discussion', value: 'Discussion' },
  { label: 'Decision', value: 'Decision' },
  { label: 'Other', value: 'Other' },
];

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
  return (
    <div className="card" style={{ padding: 20, marginTop: 16 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Meeting Details</h3>
      <div className="form-grid">
        <div className="form-field">
          <label>Meeting Title</label>
          <InputText
            value={meetingTitle}
            onChange={(e) => onMeetingTitleChange(e.target.value)}
            placeholder="Enter meeting title"
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-field">
          <label>Agenda</label>
          <Dropdown
            value={meetingAgenda}
            options={agendaOptions}
            onChange={(e) => onMeetingAgendaChange(e.value)}
            placeholder="Select Agenda"
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-field full-width">
          <label>Description</label>
          <InputTextarea
            value={meetingDescription}
            onChange={(e) => onMeetingDescriptionChange(e.target.value)}
            rows={3}
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-field">
          <label>MoM Required</label>
          <InputSwitch
            checked={momRequired}
            onChange={(e) => onMomRequiredChange(e.value)}
          />
        </div>
      </div>
    </div>
  );
}
