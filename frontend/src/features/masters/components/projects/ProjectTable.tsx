import type { ProjectDto } from '../../types';

interface ProjectTableProps {
  projects: ProjectDto[];
  isLoading: boolean;
}

export default function ProjectTable({ projects, isLoading }: ProjectTableProps) {
  if (isLoading) {
    return (
      <div style={{ padding: 20 }}>
        {[1, 2, 3].map((n) => (
          <div key={n} className="skeleton" style={{ height: 40, marginBottom: 8 }} />
        ))}
      </div>
    );
  }

  if (projects.length === 0) {
    return (
      <div className="empty-state">
        <i className="pi pi-briefcase" />
        <h3>No projects yet</h3>
        <p>Create your first project to get started</p>
      </div>
    );
  }

  return (
    <table style={{ width: '100%', borderCollapse: 'collapse' }}>
      <thead>
        <tr style={{ background: 'var(--bg-primary)' }}>
          {['Code', 'Name', 'Department', 'Incharge', 'Duration', 'Budget', 'Status'].map((h) => (
            <th
              key={h}
              style={{
                padding: '12px 16px',
                textAlign: 'left',
                fontSize: 12,
                fontWeight: 700,
                color: 'var(--text-secondary)',
                textTransform: 'uppercase',
                letterSpacing: '0.5px',
                borderBottom: '1px solid var(--border-color)',
              }}
            >
              {h}
            </th>
          ))}
        </tr>
      </thead>
      <tbody>
        {projects.map((p) => (
          <tr key={p.projectId} style={{ borderBottom: '1px solid var(--border-light)' }}>
            <td style={{ padding: '12px 16px', fontFamily: 'monospace', fontSize: 13, fontWeight: 600, color: 'var(--accent-primary)' }}>
              {p.projectCode}
            </td>
            <td style={{ padding: '12px 16px', fontWeight: 600, fontSize: 14 }}>
              {p.projectName}
            </td>
            <td style={{ padding: '12px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>
              {p.departmentName}
            </td>
            <td style={{ padding: '12px 16px', fontSize: 13 }}>
              {p.projectIncharge}
            </td>
            <td style={{ padding: '12px 16px', fontSize: 12, color: 'var(--text-muted)' }}>
              {new Date(p.startDate).toLocaleDateString()} - {new Date(p.endDate).toLocaleDateString()}
            </td>
            <td style={{ padding: '12px 16px', fontSize: 13 }}>
              {p.budgetAmount ? `₹${p.budgetAmount.toLocaleString()}` : '-'}
            </td>
            <td style={{ padding: '12px 16px' }}>
              <span
                className="badge"
                style={{
                  background: p.isActive ? 'var(--badge-emerald-bg)' : 'var(--badge-red-bg)',
                  color: p.isActive ? 'var(--badge-emerald-text)' : 'var(--badge-red-text)',
                  padding: '4px 10px',
                  borderRadius: 12,
                  fontSize: 12,
                  fontWeight: 600,
                }}
              >
                {p.isActive ? 'Active' : 'Inactive'}
              </span>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
