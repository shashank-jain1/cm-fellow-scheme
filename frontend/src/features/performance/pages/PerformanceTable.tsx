import { Tag } from 'primereact/tag';
import { EmptyState } from '../../../shared/components/ui';

const scoreColor = (score: number) => {
  if (score >= 80) return 'var(--filing)';
  if (score >= 60) return 'var(--ledger)';
  return 'var(--seal)';
};
const scoreTag = (score: number): 'success' | 'warning' | 'danger' => {
  if (score >= 80) return 'success';
  if (score >= 60) return 'warning';
  return 'danger';
};
const levelSeverity = (level: string): 'secondary' | 'info' | 'warning' | 'success' => {
  switch (level) {
    case 'Admin': return 'success'; case 'Coordinator': return 'warning';
    case 'Fellow': return 'info'; default: return 'secondary';
  }
};
const statusSeverity = (status: string): 'success' | 'warning' | 'danger' | 'info' | 'secondary' => {
  switch (status) {
    case 'Approved': return 'success'; case 'Under Review': return 'warning';
    case 'Submitted': return 'info'; case 'Rejected': return 'danger'; default: return 'secondary';
  }
};

interface Props {
  records: any[];
  isLoading: boolean;
  onRowClick: (id: number) => void;
}

export default function PerformanceTable({ records, isLoading, onRowClick }: Props) {
  const headers = ['Fellow', 'Project', 'Completion', 'Score', 'Grade', 'Level', 'Status'];
  return (
    <div className="table-wrapper">
      {isLoading ? (
        <div style={{ padding: 'var(--space-4)' }}>
          {[1, 2, 3, 4, 5].map((n) => (
            <div key={n} style={{ display: 'flex', gap: 16, padding: '10px 0', borderBottom: '1px solid var(--border)' }}>
              <div className="skeleton" style={{ width: '20%', height: 14 }} />
              <div className="skeleton" style={{ width: '20%', height: 14 }} />
              <div className="skeleton" style={{ width: '12%', height: 14 }} />
              <div className="skeleton" style={{ width: '10%', height: 14 }} />
              <div className="skeleton" style={{ width: '10%', height: 14 }} />
            </div>
          ))}
        </div>
      ) : records.length > 0 ? (
        <table style={{ width: '100%', borderCollapse: 'collapse' }}>
          <thead>
            <tr>{headers.map((h) => (
              <th key={h} style={{ padding: '8px 12px', textAlign: 'left', fontSize: 11, fontWeight: 600,
                color: 'var(--text-muted)', textTransform: 'uppercase', letterSpacing: '0.04em',
                borderBottom: '1px solid var(--border)', background: 'var(--carbon-50)' }}>{h}</th>
            ))}</tr>
          </thead>
          <tbody>
            {records.map((r) => (
              <tr key={r.performanceEvaluationId}
                style={{ borderBottom: '1px solid var(--border)', cursor: 'pointer' }}
                onClick={() => onRowClick(r.performanceEvaluationId)}>
                <td style={{ padding: '8px 12px', fontWeight: 600, fontSize: 13, color: 'var(--text-heading)' }}>{r.applicantName}</td>
                <td style={{ padding: '8px 12px', fontSize: 13, color: 'var(--text-body)' }}>{r.projectName}</td>
                <td style={{ padding: '8px 12px' }}><Tag value={`${r.completionPercentage}%`} severity={scoreTag(r.completionPercentage)} /></td>
                <td style={{ padding: '8px 12px' }}><span style={{ fontWeight: 700, fontSize: 14, color: scoreColor(r.performanceScore) }}>{r.performanceScore}</span></td>
                <td style={{ padding: '8px 12px', fontSize: 13, fontWeight: 700, color: 'var(--text-heading)' }}>{r.performanceGrade}</td>
                <td style={{ padding: '8px 12px' }}><Tag value={r.reviewLevel ?? 'Draft'} severity={levelSeverity(r.reviewLevel ?? 'Draft')} /></td>
                <td style={{ padding: '8px 12px' }}><Tag value={r.reviewStatus ?? 'Draft'} severity={statusSeverity(r.reviewStatus ?? 'Draft')} /></td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <EmptyState icon="pi pi-chart-bar" title="No performance records" description="Performance data will appear here after reviews" />
      )}
    </div>
  );
}
