import { Tag } from 'primereact/tag';
import { EmptyState } from '../../../shared/components/ui';
import type { ReviewHistoryDto } from '../types';

interface Props {
  history: ReviewHistoryDto[];
}

export default function ReviewHistoryList({ history }: Props) {
  return (
    <div className="card" style={{ padding: 24, marginBottom: 20 }}>
      <h3 className="form-section-header">Review History</h3>
      {history.length > 0 ? (
        <div style={{ display: 'flex', flexDirection: 'column', gap: 12 }}>
          {history.map((h) => (
            <div key={h.performanceReviewHistoryId} style={{
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'flex-start',
              padding: '12px 16px',
              borderRadius: 'var(--radius-md)',
              background: 'var(--bg-secondary)',
              border: '1px solid var(--border-light)',
            }}>
              <div>
                <div style={{ display: 'flex', alignItems: 'center', gap: 8, marginBottom: 4 }}>
                  <Tag value={h.action} severity={h.action === 'Approve' ? 'success' : h.action === 'Reject' ? 'danger' : 'info'} />
                  <span style={{ fontSize: 13, fontWeight: 600, color: 'var(--text-primary)' }}>
                    {h.previousLevel} → {h.newLevel}
                  </span>
                </div>
                <div style={{ fontSize: 12, color: 'var(--text-secondary)' }}>
                  by {h.performedBy ?? 'Unknown'} — {new Date(h.performedOn).toLocaleString()}
                </div>
                {h.remarks && (
                  <div style={{ fontSize: 12, color: 'var(--text-muted)', marginTop: 4 }}>{h.remarks}</div>
                )}
              </div>
            </div>
          ))}
        </div>
      ) : (
        <EmptyState icon="pi pi-history" title="No review history" description="Review actions will appear here" />
      )}
    </div>
  );
}
