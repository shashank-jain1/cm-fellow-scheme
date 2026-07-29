import { useRef } from 'react';
import { AppInput, AppTextarea, AppSwitch, AppMultiSelect, AppSelect } from '../../../shared/components/forms';
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
          <AppInput
            value={meetingTitle}
            onChange={(e) => onMeetingTitleChange(e.target.value)}
            placeholder="Enter meeting title"
          />
        </div>
        <div className="form-field">
          <label>Meeting Agenda *</label>
          <AppSelect
            value={meetingAgenda}
            options={agendaOptions}
            onChange={(v) => onMeetingAgendaChange(v)}
            placeholder="Select agenda"
          />
        </div>
        <div className="form-field full-width">
          <label>Meeting Description *</label>
          <AppTextarea
            value={meetingDescription}
            onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onMeetingDescriptionChange(e.target.value)}
            rows={3}
            placeholder="Enter meeting description"
          />
        </div>
        <div className="form-field">
          <label>Conduct Person *</label>
          <AppSelect
            value={conductPersonId}
            options={userOptions}
            onChange={(v) => onConductPersonChange(v)}
            placeholder="Select conduct person"
          />
        </div>
        <div className="form-field">
          <label>Coordinator *</label>
          <AppSelect
            value={coordinatorId}
            options={userOptions}
            onChange={(v) => onCoordinatorChange(v)}
            placeholder="Select coordinator"
          />
        </div>
        <div className="form-field full-width">
          <label>Participants *</label>
          <AppMultiSelect
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
            <AppSwitch
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
              <button
                type="button"
                onClick={() => onMeetingAttachmentChange(null)}
                className="btn btn-ghost btn-sm"
                style={{ width: 32, height: 32, padding: 0, display: 'inline-flex', alignItems: 'center', justifyContent: 'center', color: 'var(--danger)' }}
              >
                <i className="pi pi-times" />
              </button>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
