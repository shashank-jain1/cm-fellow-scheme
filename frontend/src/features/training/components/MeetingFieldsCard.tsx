import { useListUserAccounts } from '../../registration/queries/user-management';
import type { UserAccountListItem } from '../../registration/types/user-management';
import MeetingAgendaField from './MeetingAgendaField';
import MeetingAttendeesField from './MeetingAttendeesField';

interface Props {
  meetingTitle: string;
  meetingAgenda: string;
  meetingDescription: string;
  conductPersonId: number;
  coordinatorId: number;
  participantIds: number[];
  momRequired: boolean;
  meetingAttachmentFile: File | null;
  onMeetingTitleChange: (v: string) => void;
  onMeetingAgendaChange: (v: string) => void;
  onMeetingDescriptionChange: (v: string) => void;
  onConductPersonChange: (v: number) => void;
  onCoordinatorChange: (v: number) => void;
  onParticipantsChange: (v: number[]) => void;
  onMomRequiredChange: (v: boolean) => void;
  onMeetingAttachmentChange: (v: File | null) => void;
}

function userToOption(u: UserAccountListItem) {
  return { label: `${u.firstName} ${u.lastName} (${u.role})`, value: u.userAccountId };
}

export default function MeetingFieldsCard(props: Props) {
  const { data: users = [] } = useListUserAccounts(undefined, true);
  const userOptions = users.map(userToOption);

  return (
    <div className="glass-card" style={{ padding: 24, marginTop: 20 }}>
      <h3 className="form-section-header">Meeting Details</h3>
      <div className="form-grid">
        <MeetingAgendaField
          meetingTitle={props.meetingTitle} meetingAgenda={props.meetingAgenda}
          meetingDescription={props.meetingDescription} conductPersonId={props.conductPersonId}
          coordinatorId={props.coordinatorId} userOptions={userOptions}
          onMeetingTitleChange={props.onMeetingTitleChange} onMeetingAgendaChange={props.onMeetingAgendaChange}
          onMeetingDescriptionChange={props.onMeetingDescriptionChange}
          onConductPersonChange={props.onConductPersonChange} onCoordinatorChange={props.onCoordinatorChange} />
        <MeetingAttendeesField
          participantIds={props.participantIds} momRequired={props.momRequired}
          meetingAttachmentFile={props.meetingAttachmentFile} userOptions={userOptions}
          onParticipantsChange={props.onParticipantsChange} onMomRequiredChange={props.onMomRequiredChange}
          onMeetingAttachmentChange={props.onMeetingAttachmentChange} />
      </div>
    </div>
  );
}
