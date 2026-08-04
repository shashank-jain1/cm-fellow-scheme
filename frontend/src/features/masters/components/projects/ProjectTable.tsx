import type { ProjectDto } from '../../types';

interface ProjectTableProps {
  projects: ProjectDto[];
  isLoading: boolean;
  onEdit?: (project: ProjectDto) => void;
  onDelete?: (projectId: number) => void;
}

export default function ProjectTable({ projects, isLoading, onEdit, onDelete }: ProjectTableProps) {
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
          {['Code', 'Name', 'Department', 'Incharge', 'Duration', 'Budget', 'Status', 'Actions'].map((h) => (
            <th
              key={h}
              style={{
                padding: '12px 16px',
                textAlign: h === 'Actions' ? 'center' : 'left',
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
            <td style={{ padding: '12px 16px', textAlign: 'center' }}>
              <div style={{ display: 'flex', gap: 6, justifyContent: 'center' }}>
                {onEdit && (
                  <button className="btn btn-secondary btn-icon" onClick={() => onEdit(p)} title="Edit Project">
                    <i className="pi pi-pencil" style={{ fontSize: 12 }} />
                  </button>
                )}
                {onDelete && (
                  <button className="btn btn-danger btn-icon" onClick={() => onDelete(p.projectId)} title="Delete Project">
                    <i className="pi pi-trash" style={{ fontSize: 12 }} />
                  </button>
                )}
              </div>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
