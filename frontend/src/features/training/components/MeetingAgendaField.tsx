import { AppInput, AppTextarea, AppSelect } from '../../../shared/components/forms';
import { useLookupOptions } from '../../../shared/hooks/useMasters';

interface Props {
  meetingTitle: string;
  meetingAgenda: string;
  meetingDescription: string;
  conductPersonId: number;
  coordinatorId: number;
  userOptions: { label: string; value: number }[];
  onMeetingTitleChange: (v: string) => void;
  onMeetingAgendaChange: (v: string) => void;
  onMeetingDescriptionChange: (v: string) => void;
  onConductPersonChange: (v: number) => void;
  onCoordinatorChange: (v: number) => void;
}

export default function MeetingAgendaField({
  meetingTitle, meetingAgenda, meetingDescription,
  conductPersonId, coordinatorId, userOptions,
  onMeetingTitleChange, onMeetingAgendaChange, onMeetingDescriptionChange,
  onConductPersonChange, onCoordinatorChange,
}: Props) {
  const agendaOptions = useLookupOptions('MeetingAgenda');

  return (
    <>
      <div className="form-field">
        <label>Meeting Title *</label>
        <AppInput value={meetingTitle} onChange={(e) => onMeetingTitleChange(e.target.value)}
          placeholder="Enter meeting title" />
      </div>
      <div className="form-field">
        <label>Meeting Agenda *</label>
        <AppSelect value={meetingAgenda} options={agendaOptions}
          onChange={(v) => onMeetingAgendaChange(v)} placeholder="Select agenda" />
      </div>
      <div className="form-field full-width">
        <label>Meeting Description *</label>
        <AppTextarea value={meetingDescription}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onMeetingDescriptionChange(e.target.value)}
          rows={3} placeholder="Enter meeting description" />
      </div>
      <div className="form-field">
        <label>Conduct Person *</label>
        <AppSelect value={conductPersonId} options={userOptions}
          onChange={(v) => onConductPersonChange(v)} placeholder="Select conduct person" />
      </div>
      <div className="form-field">
        <label>Coordinator *</label>
        <AppSelect value={coordinatorId} options={userOptions}
          onChange={(v) => onCoordinatorChange(v)} placeholder="Select coordinator" />
      </div>
    </>
  );
}
