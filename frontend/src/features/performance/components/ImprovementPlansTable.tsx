import { Tag } from 'primereact/tag';
import { EmptyState, SkeletonTable } from '../../../shared/components/ui';
import type { ImprovementPlanDto } from '../types';

const statusSeverity: Record<string, 'success' | 'info' | 'warning' | 'danger' | 'secondary'> = {
  NotStarted: 'secondary',
  InProgress: 'info',
  Completed: 'success',
  Cancelled: 'danger',
};

interface ImprovementPlansTableProps {
  plans: ImprovementPlanDto[];
  isLoading: boolean;
}

export default function ImprovementPlansTable({ plans, isLoading }: ImprovementPlansTableProps) {
  if (isLoading) return <SkeletonTable columns={5} />;

  if (plans.length === 0) {
    return <EmptyState icon="pi pi-clipboard" title="No improvement plans" description="No improvement plans have been created yet" />;
  }

  return (
    <table style={{ width: '100%', borderCollapse: 'collapse' }}>
      <thead>
        <tr>
          {['Title', 'Description', 'Start Date', 'End Date', 'Status'].map((h) => (
            <th key={h} style={{ padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase', letterSpacing: '0.5px', borderBottom: '1px solid var(--border-color)' }}>{h}</th>
          ))}
        </tr>
      </thead>
      <tbody>
        {plans.map((p) => (
          <tr key={p.improvementPlanId} style={{ borderBottom: '1px solid var(--border-light)' }}>
            <td style={{ padding: '14px 16px', fontWeight: 600, fontSize: 14, color: 'var(--text-primary)' }}>{p.title}</td>
            <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-secondary)', maxWidth: 300, overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap' }}>{p.description}</td>
            <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{new Date(p.startDate).toLocaleDateString()}</td>
            <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{new Date(p.endDate).toLocaleDateString()}</td>
            <td style={{ padding: '14px 16px' }}><Tag value={p.status} severity={statusSeverity[p.status]} /></td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
