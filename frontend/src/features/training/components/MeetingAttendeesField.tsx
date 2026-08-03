import { useRef } from 'react';
import { AppMultiSelect, AppSwitch } from '../../../shared/components/forms';

interface Props {
  participantIds: number[];
  momRequired: boolean;
  meetingAttachmentFile: File | null;
  userOptions: { label: string; value: number }[];
  onParticipantsChange: (v: number[]) => void;
  onMomRequiredChange: (v: boolean) => void;
  onMeetingAttachmentChange: (v: File | null) => void;
}

export default function MeetingAttendeesField({
  participantIds, momRequired, meetingAttachmentFile, userOptions,
  onParticipantsChange, onMomRequiredChange, onMeetingAttachmentChange,
}: Props) {
  const fileInputRef = useRef<HTMLInputElement>(null);

  return (
    <>
      <div className="form-field full-width">
        <label>Participants *</label>
        <AppMultiSelect value={participantIds} options={userOptions}
          onChange={(e) => onParticipantsChange(e.value ?? [])}
          placeholder="Select Participants" display="chip" filter className="w-full" />
      </div>
      <div className="form-field">
        <label>MOM Required</label>
        <div style={{ display: 'flex', alignItems: 'center', gap: 10, height: 40 }}>
          <AppSwitch checked={momRequired} onChange={(e) => onMomRequiredChange(e.value ?? false)} />
          <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>{momRequired ? 'Yes' : 'No'}</span>
        </div>
      </div>
      <div className="form-field full-width">
        <label>Attachment (PDF, DOC, XLSX, PPT — max 20MB)</label>
        <input ref={fileInputRef} type="file" accept=".pdf,.doc,.docx,.xlsx,.ppt,.pptx" style={{ display: 'none' }}
          onChange={(e) => onMeetingAttachmentChange(e.target.files?.[0] ?? null)} />
        <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
          <button type="button" className="file-upload-btn" onClick={() => fileInputRef.current?.click()}>
            <i className={`pi ${meetingAttachmentFile ? 'pi-check-circle' : 'pi-upload'}`}
              style={{ color: meetingAttachmentFile ? 'var(--accent-primary)' : 'var(--text-secondary)' }} />
            {meetingAttachmentFile ? meetingAttachmentFile.name : 'Choose File'}
          </button>
          {meetingAttachmentFile && (
            <button type="button" onClick={() => onMeetingAttachmentChange(null)}
              className="btn btn-ghost btn-sm"
              style={{ width: 32, height: 32, padding: 0, display: 'inline-flex', alignItems: 'center', justifyContent: 'center', color: 'var(--danger)' }}>
              <i className="pi pi-times" />
            </button>
          )}
        </div>
      </div>
    </>
  );
}
