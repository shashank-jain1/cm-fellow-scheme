import { useRef } from 'react';
import { InputText } from 'primereact/inputtext';
import { InputTextarea } from 'primereact/inputtextarea';
import { InputSwitch } from 'primereact/inputswitch';
import { Dropdown } from 'primereact/dropdown';
import { MultiSelect } from 'primereact/multiselect';
import { Button } from 'primereact/button';
import { useLookupOptions } from '../../../shared/hooks/useMasters';
import { useListUserAccounts } from '../../registration/queries/user-management';
import type { UserAccountListItem } from '../../registration/types/user-management';

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

export default function MeetingFieldsCard({
  meetingTitle,
  meetingAgenda,
  meetingDescription,
  conductPersonId,
  coordinatorId,
  participantIds,
  momRequired,
  meetingAttachmentFile,
  onMeetingTitleChange,
  onMeetingAgendaChange,
  onMeetingDescriptionChange,
  onConductPersonChange,
  onCoordinatorChange,
  onParticipantsChange,
  onMomRequiredChange,
  onMeetingAttachmentChange,
}: Props) {
  const agendaOptions = useLookupOptions('MeetingAgenda');
  const { data: users = [] } = useListUserAccounts(undefined, true);
  const fileInputRef = useRef<HTMLInputElement>(null);

  const userOptions = users.map(userToOption);

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
          <label>Meeting Agenda *</label>
          <Dropdown
            value={meetingAgenda || undefined}
            options={agendaOptions}
            onChange={(e) => onMeetingAgendaChange(e.value ?? '')}
            placeholder="Select Agenda"
            className="w-full"
          />
        </div>
        <div className="form-field full-width">
          <label>Meeting Description *</label>
          <InputTextarea
            value={meetingDescription}
            onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onMeetingDescriptionChange(e.target.value)}
            rows={3}
            placeholder="Enter meeting description"
          />
        </div>
        <div className="form-field">
          <label>Conduct Person *</label>
          <Dropdown
            value={conductPersonId || undefined}
            options={userOptions}
            onChange={(e) => onConductPersonChange(e.value ?? 0)}
            placeholder="Select Conduct Person"
            filter
            className="w-full"
          />
        </div>
        <div className="form-field">
          <label>Coordinator *</label>
          <Dropdown
            value={coordinatorId || undefined}
            options={userOptions}
            onChange={(e) => onCoordinatorChange(e.value ?? 0)}
            placeholder="Select Coordinator"
            filter
            className="w-full"
          />
        </div>
        <div className="form-field full-width">
          <label>Participants *</label>
          <MultiSelect
            value={participantIds}
            options={userOptions}
            onChange={(e) => onParticipantsChange(e.value ?? [])}
            placeholder="Select Participants"
            display="chip"
            filter
            className="w-full"
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
        <div className="form-field full-width">
          <label>Attachment (PDF, DOC, XLSX, PPT — max 20MB)</label>
          <input
            ref={fileInputRef}
            type="file"
            accept=".pdf,.doc,.docx,.xlsx,.ppt,.pptx"
            style={{ display: 'none' }}
            onChange={(e) => onMeetingAttachmentChange(e.target.files?.[0] ?? null)}
          />
          <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
            <button
              type="button"
              className="file-upload-btn"
              onClick={() => fileInputRef.current?.click()}
            >
              <i className={`pi ${meetingAttachmentFile ? 'pi-check-circle' : 'pi-upload'}`}
                 style={{ color: meetingAttachmentFile ? 'var(--accent-primary)' : 'var(--text-secondary)' }} />
              {meetingAttachmentFile ? meetingAttachmentFile.name : 'Choose File'}
            </button>
            {meetingAttachmentFile && (
              <Button
                icon="pi pi-times"
                severity="danger"
                text
                rounded
                onClick={() => onMeetingAttachmentChange(null)}
                type="button"
                style={{ width: 32, height: 32 }}
              />
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
