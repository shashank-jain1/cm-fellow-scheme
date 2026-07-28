import { Tag } from 'primereact/tag';

const REVIEW_LEVEL_ORDER = ['Draft', 'Fellow', 'Coordinator', 'Admin'];

const levelSeverity = (level: string): 'secondary' | 'info' | 'warning' | 'success' | 'danger' => {
  switch (level) {
    case 'Admin': return 'success';
    case 'Coordinator': return 'warning';
    case 'Fellow': return 'info';
    case 'Draft': return 'secondary';
    default: return 'secondary';
  }
};

const statusSeverity = (status: string): 'success' | 'warning' | 'danger' | 'info' | 'secondary' => {
  switch (status) {
    case 'Approved': return 'success';
    case 'Under Review': return 'warning';
    case 'Submitted': return 'info';
    case 'Rejected': return 'danger';
    default: return 'secondary';
  }
};

interface Props {
  reviewLevel: string;
  reviewStatus: string;
}

export default function ReviewChainProgress({ reviewLevel, reviewStatus }: Props) {
  const currentLevelIndex = REVIEW_LEVEL_ORDER.indexOf(reviewLevel ?? 'Draft');

  return (
    <div className="card" style={{ padding: 24, marginBottom: 20 }}>
      <h3 className="form-section-header">Review Chain</h3>
      <div style={{ display: 'flex', alignItems: 'center', gap: 8, marginBottom: 20 }}>
        {REVIEW_LEVEL_ORDER.map((level, i) => (
          <div key={level} style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
            <div style={{
              padding: '6px 14px',
              borderRadius: 20,
              fontSize: 12,
              fontWeight: 700,
              background: i <= currentLevelIndex ? 'var(--accent-primary)' : 'var(--bg-secondary)',
              color: i <= currentLevelIndex ? 'white' : 'var(--text-muted)',
            }}>
              {level}
            </div>
            {i < REVIEW_LEVEL_ORDER.length - 1 && (
              <i className="pi pi-arrow-right" style={{ fontSize: 12, color: 'var(--text-muted)' }} />
            )}
          </div>
        ))}
      </div>
      <div style={{ display: 'flex', gap: 12 }}>
        <div>
          <label style={{ fontSize: 12, fontWeight: 600, color: 'var(--text-secondary)', display: 'block', marginBottom: 4 }}>Current Level</label>
          <Tag value={reviewLevel ?? 'Draft'} severity={levelSeverity(reviewLevel ?? 'Draft')} />
        </div>
        <div>
          <label style={{ fontSize: 12, fontWeight: 600, color: 'var(--text-secondary)', display: 'block', marginBottom: 4 }}>Status</label>
          <Tag value={reviewStatus ?? 'Draft'} severity={statusSeverity(reviewStatus ?? 'Draft')} />
        </div>
      </div>
    </div>
  );
}
