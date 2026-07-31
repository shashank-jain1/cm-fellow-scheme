import { AppButton } from '../../../shared/components/ui';
import { formatDate } from '../../../shared/utils/format';
import type { TaskAttachment } from '../types';

const formatFileSize = (bytes: number) => {
  if (bytes < 1024) return `${bytes} B`;
  if (bytes < 1024 * 1024) return `${(bytes / 1024).toFixed(1)} KB`;
  return `${(bytes / (1024 * 1024)).toFixed(1)} MB`;
};

interface AttachmentListProps {
  attachments: TaskAttachment[];
  isLoading: boolean;
}

export default function AttachmentList({ attachments, isLoading }: AttachmentListProps) {
  if (isLoading) {
    return (
      <div style={{ padding: 12 }}>
        {[1, 2, 3].map((n) => (
          <div key={n} style={{ display: 'flex', gap: 12, padding: '10px 0', borderBottom: '1px solid var(--border-light)' }}>
            <div className="skeleton" style={{ width: '35%', height: 14 }} />
            <div className="skeleton" style={{ width: '20%', height: 14 }} />
            <div className="skeleton" style={{ width: '15%', height: 14 }} />
          </div>
        ))}
      </div>
    );
  }

  if (attachments.length === 0) {
    return (
      <div style={{ padding: 24, textAlign: 'center', color: 'var(--text-muted)', fontSize: 14 }}>No deliverables uploaded yet</div>
    );
  }

  return (
    <div style={{ display: 'flex', flexDirection: 'column' }}>
      {attachments.map((attachment) => (
        <div key={attachment.attachmentId} style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', padding: '12px 16px', borderBottom: '1px solid var(--border-light)' }}>
          <div style={{ display: 'flex', alignItems: 'center', gap: 12, flex: 1 }}>
            <i className="pi pi-file" style={{ fontSize: 18, color: 'var(--text-muted)' }} />
            <div>
              <div style={{ fontSize: 14, fontWeight: 500 }}>{attachment.fileName}</div>
              <div style={{ fontSize: 12, color: 'var(--text-muted)' }}>
                {formatFileSize(attachment.fileSize)} &middot; {attachment.uploadedByName} &middot; {formatDate(attachment.uploadedAt)}
              </div>
            </div>
          </div>
          <AppButton size="sm" variant="ghost" icon="pi pi-download" onClick={() => window.open(attachment.fileUrl, '_blank')} title="Download" />
        </div>
      ))}
    </div>
  );
}
