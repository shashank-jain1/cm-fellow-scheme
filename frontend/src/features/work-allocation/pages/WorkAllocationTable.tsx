import type { WorkAllocationDto } from '../types';
import { Tag } from 'primereact/tag';
import { AppButton } from '../../../shared/components/ui';

const prioritySeverity = (p: string) => {
  switch (p.toLowerCase()) {
    case 'high': return 'danger';
    case 'medium': return 'warning';
    case 'low': return 'info';
    default: return 'secondary';
  }
};

interface Props {
  allocations: WorkAllocationDto[];
  isLoading: boolean;
  onEdit: (a: WorkAllocationDto) => void;
  onDelete: (id: number) => void;
}

export default function WorkAllocationTable({ allocations, isLoading, onEdit, onDelete }: Props) {
  if (isLoading) {
    return (
      <div style={{ padding: 'var(--space-4)' }}>
        {[1, 2, 3, 4, 5].map((n) => (
          <div key={n} style={{ display: 'flex', gap: 16, padding: '12px 0', borderBottom: '1px solid var(--border)' }}>
            <div className="skeleton" style={{ width: '15%', height: 14 }} />
            <div className="skeleton" style={{ width: '20%', height: 14 }} />
            <div className="skeleton" style={{ width: '15%', height: 14 }} />
            <div className="skeleton" style={{ width: '12%', height: 14 }} />
            <div className="skeleton" style={{ width: '10%', height: 14 }} />
          </div>
        ))}
      </div>
    );
  }

  if (allocations.length === 0) {
    return (
      <div className="empty-state">
        <i className="pi pi-briefcase" />
        <h3>No allocations found</h3>
        <p>Create work allocations to assign tasks to CM Fellows</p>
      </div>
    );
  }

  return (
    <table style={{ width: '100%', borderCollapse: 'collapse' }}>
      <thead>
        <tr>
          {['Description', 'Priority', 'Duration', 'Surveys', 'Completion', 'Status', 'Actions'].map((h) => (
            <th key={h} style={{ padding: '10px 16px', textAlign: 'left', fontSize: 12, fontWeight: 600, color: 'var(--text-muted)', textTransform: 'uppercase', letterSpacing: '0.04em', borderBottom: '1px solid var(--border)', background: 'var(--carbon-50)' }}>{h}</th>
          ))}
        </tr>
      </thead>
      <tbody>
        {allocations.map((a) => (
          <tr key={a.workAllocationId} style={{ borderBottom: '1px solid var(--border)' }}>
            <td style={{ padding: '12px 16px', fontWeight: 600, fontSize: 14, color: 'var(--text-heading)' }}>{a.workDescription}</td>
            <td style={{ padding: '12px 16px' }}><Tag value={a.priority} severity={prioritySeverity(a.priority)} /></td>
            <td style={{ padding: '12px 16px', fontSize: 13, color: 'var(--text-muted)' }}>{a.startDate} - {a.endDate}</td>
            <td style={{ padding: '12px 16px', fontSize: 14 }}>{a.surveysPerIntern}</td>
            <td style={{ padding: '12px 16px' }}>
              <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
                <div style={{ width: 60, height: 4, borderRadius: 2, background: 'var(--border)' }}>
                  <div style={{ width: `${a.completionPercentage}%`, height: '100%', borderRadius: 2, background: a.completionPercentage >= 80 ? 'var(--success)' : 'var(--pending)' }} />
                </div>
                <span style={{ fontSize: 12, color: 'var(--text-muted)' }}>{a.completionPercentage}%</span>
              </div>
            </td>
            <td style={{ padding: '12px 16px' }}><Tag value={a.status} severity={a.status === 'active' ? 'success' : a.status === 'pending' ? 'warning' : 'secondary'} /></td>
            <td style={{ padding: '12px 16px' }}>
              <div style={{ display: 'flex', gap: 8 }}>
                <AppButton variant="ghost" size="sm" icon="pi pi-pencil" onClick={() => onEdit(a)} title="Edit" />
                <AppButton variant="ghost" size="sm" icon="pi pi-trash" onClick={() => onDelete(a.workAllocationId)} title="Delete" />
              </div>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
