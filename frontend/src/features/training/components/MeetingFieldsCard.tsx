import { InputText } from 'primereact/inputtext';
import { InputTextarea } from 'primereact/inputtextarea';
import { InputSwitch } from 'primereact/inputswitch';
import FormSelect from '../../../shared/components/FormSelect';
import { useLookupOptions } from '../../../shared/hooks/useMasters';

interface Participant {
  applicantId: number;
  name: string;
}

interface Props {
  meetingTitle: string;
  meetingAgenda: string;
  meetingDescription: string;
  conductPersonId: string;
  coordinatorId: string;
  participants: Participant[];
  momRequired: boolean;
  onMeetingTitleChange: (v: string) => void;
  onMeetingAgendaChange: (v: string) => void;
  onMeetingDescriptionChange: (v: string) => void;
  onConductPersonChange: (v: string) => void;
  onCoordinatorChange: (v: string) => void;
  onParticipantsChange: (v: Participant[]) => void;
  onMomRequiredChange: (v: boolean) => void;
}

export default function MeetingFieldsCard({
  meetingTitle,
  meetingAgenda,
  meetingDescription,
  conductPersonId,
  coordinatorId,
  participants,
  momRequired,
  onMeetingTitleChange,
  onMeetingAgendaChange,
  onMeetingDescriptionChange,
  onConductPersonChange,
  onCoordinatorChange,
  onParticipantsChange,
  onMomRequiredChange,
}: Props) {
  const agendaOptions = useLookupOptions('MeetingAgenda');

  const handleParticipantToggle = (applicantId: number, name: string) => {
    const exists = participants.some(p => p.applicantId === applicantId);
    if (exists) {
      onParticipantsChange(participants.filter(p => p.applicantId !== applicantId));
    } else {
      onParticipantsChange([...participants, { applicantId, name }]);
    }
  };

  return (
    <div className="glass-card" style={{ padding: 24, marginTop: 20 }}>
      <h3 className="form-section-header">Meeting Details</h3>
      <div className="form-grid">
        <div className="form-field">
          <label>Meeting Title *</label>
          <InputText
            value={meetingTitle}
            onChange={(e) => onMeetingTitleChange(e.target.value)}
            placeholder="Enter meeting title"
          />
        </div>
        <div className="form-field">
          <label>Agenda *</label>
          <FormSelect
            value={meetingAgenda}
            options={agendaOptions}
            onChange={(val) => onMeetingAgendaChange(val)}
            placeholder="Select Agenda"
          />
        </div>
        <div className="form-field full-width">
          <label>Description *</label>
          <InputTextarea
            value={meetingDescription}
            onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onMeetingDescriptionChange(e.target.value)}
            rows={3}
            placeholder="Enter meeting description"
          />
        </div>
        <div className="form-field">
          <label>Conduct Person *</label>
          <InputText
            value={conductPersonId}
            onChange={(e) => onConductPersonChange(e.target.value)}
            placeholder="Select conduct person"
          />
        </div>
        <div className="form-field">
          <label>Coordinator *</label>
          <InputText
            value={coordinatorId}
            onChange={(e) => onCoordinatorChange(e.target.value)}
            placeholder="Select coordinator"
          />
        </div>
        <div className="form-field full-width">
          <label>Participants *</label>
          <div style={{ display: 'flex', flexWrap: 'wrap', gap: 8 }}>
            {participants.length > 0 ? (
              participants.map(p => (
                <span
                  key={p.applicantId}
                  style={{
                    padding: '4px 8px',
                    background: 'var(--primary-color)',
                    color: 'white',
                    borderRadius: 4,
                    fontSize: 12,
                  }}
                >
                  {p.name}
                </span>
              ))
            ) : (
              <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>No participants selected</span>
            )}
          </div>
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
