import type { WorkAllocationDto } from '../types';

interface Props {
  allocations: WorkAllocationDto[];
  completedCount: number;
  totalCount: number;
}

export default function WorkAllocationStats({ allocations, completedCount, totalCount }: Props) {
  const active = allocations.filter(a => a.status === 'active').length;
  const pending = allocations.filter(a => a.status === 'pending').length;

  return (
    <div style={{ display: 'flex', gap: 16, marginBottom: 'var(--space-5)' }}>
      <div style={{ flex: 1, padding: 16, background: 'var(--surface)', borderRadius: 8, border: '1px solid var(--border)' }}>
        <div style={{ fontSize: 12, color: 'var(--text-muted)', marginBottom: 4 }}>Total Allocations</div>
        <div style={{ fontSize: 24, fontWeight: 700 }}>{totalCount}</div>
      </div>
      <div style={{ flex: 1, padding: 16, background: 'var(--surface)', borderRadius: 8, border: '1px solid var(--border)' }}>
        <div style={{ fontSize: 12, color: 'var(--text-muted)', marginBottom: 4 }}>Active</div>
        <div style={{ fontSize: 24, fontWeight: 700, color: 'var(--success)' }}>{active}</div>
      </div>
      <div style={{ flex: 1, padding: 16, background: 'var(--surface)', borderRadius: 8, border: '1px solid var(--border)' }}>
        <div style={{ fontSize: 12, color: 'var(--text-muted)', marginBottom: 4 }}>Pending</div>
        <div style={{ fontSize: 24, fontWeight: 700, color: 'var(--warning)' }}>{pending}</div>
      </div>
      <div style={{ flex: 1, padding: 16, background: 'var(--surface)', borderRadius: 8, border: '1px solid var(--border)' }}>
        <div style={{ fontSize: 12, color: 'var(--text-muted)', marginBottom: 4 }}>Completed</div>
        <div style={{ fontSize: 24, fontWeight: 700 }}>{completedCount}</div>
      </div>
    </div>
  );
}
