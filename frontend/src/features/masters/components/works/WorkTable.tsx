import type { WorkDto, ProjectDto } from '../../types';

interface WorkTableProps {
  works: WorkDto[];
  projects: ProjectDto[];
  isLoading: boolean;
  selectedProjectId: number | null;
}

const badgeStyle = (active: boolean) => ({
  background: active ? 'var(--badge-emerald-bg)' : 'var(--badge-red-bg)',
  color: active ? 'var(--badge-emerald-text)' : 'var(--badge-red-text)',
  padding: '4px 10px',
  borderRadius: 12,
  fontSize: 12,
  fontWeight: 600,
});

const priorityBadge = (p: string) => ({
  background: p === 'High' ? 'var(--badge-red-bg)' : p === 'Low' ? 'var(--badge-emerald-bg)' : 'var(--badge-amber-bg)',
  color: p === 'High' ? 'var(--badge-red-text)' : p === 'Low' ? 'var(--badge-emerald-text)' : 'var(--badge-amber-text)',
  padding: '4px 10px',
  borderRadius: 12,
  fontSize: 12,
  fontWeight: 600,
});

const thStyle = { padding: '12px 16px', textAlign: 'left' as const, fontSize: 12, fontWeight: 700, color: 'var(--text-secondary)', textTransform: 'uppercase' as const, letterSpacing: '0.5px', borderBottom: '1px solid var(--border-color)' };

export default function WorkTable({ works, projects, isLoading, selectedProjectId }: WorkTableProps) {
  if (isLoading) {
    return (
      <div style={{ padding: 20 }}>
        {[1, 2, 3].map((n) => <div key={n} className="skeleton" style={{ height: 40, marginBottom: 8 }} />)}
      </div>
    );
  }
  if (works.length === 0) {
    return (
      <div className="empty-state">
        <i className="pi pi-briefcase" />
        <h3>No works yet</h3>
        <p>{selectedProjectId ? 'No works for this project' : 'Select a project or create your first work'}</p>
      </div>
    );
  }
  return (
    <table style={{ width: '100%', borderCollapse: 'collapse' }}>
      <thead>
        <tr style={{ background: 'var(--bg-primary)' }}>
          {['Work Name', 'Project', 'Priority', 'Assigned To', 'Duration', 'Status'].map((h) => (
            <th key={h} style={thStyle}>{h}</th>
          ))}
        </tr>
      </thead>
      <tbody>
        {works.map((w) => {
          const project = projects.find((p) => p.projectId === w.projectId);
          return (
            <tr key={w.workId} style={{ borderBottom: '1px solid var(--border-light)' }}>
              <td style={{ padding: '12px 16px', fontWeight: 600, fontSize: 14 }}>{w.workName}</td>
              <td style={{ padding: '12px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{project?.projectName ?? w.projectId}</td>
              <td style={{ padding: '12px 16px' }}>
                <span className="badge" style={priorityBadge(w.priority)}>{w.priority}</span>
              </td>
              <td style={{ padding: '12px 16px', fontSize: 13 }}>{w.assignedTo}</td>
              <td style={{ padding: '12px 16px', fontSize: 12, color: 'var(--text-muted)' }}>
                {new Date(w.startDate).toLocaleDateString()} - {new Date(w.endDate).toLocaleDateString()}
              </td>
              <td style={{ padding: '12px 16px' }}>
                <span className="badge" style={badgeStyle(w.isActive)}>{w.isActive ? 'Active' : 'Inactive'}</span>
              </td>
            </tr>
          );
        })}
      </tbody>
    </table>
  );
}
