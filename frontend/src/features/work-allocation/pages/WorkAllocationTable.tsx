import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import type { WorkAllocationDto } from '../types';
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
    <DataTable
      value={allocations}
      responsiveLayout="scroll"
      emptyMessage="No allocations found"
      dataKey="workAllocationId"
      loading={isLoading}
    >
      <Column field="workDescription" header="Description" bodyStyle={{ fontWeight: 600, fontSize: 14, color: 'var(--text-heading)' }} />
      <Column
        field="priority"
        header="Priority"
        body={(row: WorkAllocationDto) => <Tag value={row.priority} severity={prioritySeverity(row.priority)} />}
      />
      <Column
        header="Duration"
        body={(row: WorkAllocationDto) => <span style={{ fontSize: 13, color: 'var(--text-muted)' }}>{row.startDate} - {row.endDate}</span>}
      />
      <Column field="surveysPerIntern" header="Surveys" bodyStyle={{ fontSize: 14 }} />
      <Column
        header="Completion"
        body={(row: WorkAllocationDto) => (
          <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
            <div style={{ width: 60, height: 4, borderRadius: 2, background: 'var(--border)' }}>
              <div style={{ width: `${row.completionPercentage}%`, height: '100%', borderRadius: 2, background: row.completionPercentage >= 80 ? 'var(--success)' : 'var(--pending)' }} />
            </div>
            <span style={{ fontSize: 12, color: 'var(--text-muted)' }}>{row.completionPercentage}%</span>
          </div>
        )}
      />
      <Column
        field="status"
        header="Status"
        body={(row: WorkAllocationDto) => <Tag value={row.status} severity={row.status === 'active' ? 'success' : row.status === 'pending' ? 'warning' : 'secondary'} />}
      />
      <Column
        header="Actions"
        body={(row: WorkAllocationDto) => (
          <div style={{ display: 'flex', gap: 8 }}>
            <AppButton variant="ghost" size="sm" icon="pi pi-pencil" onClick={() => onEdit(row)} title="Edit" />
            <AppButton variant="ghost" size="sm" icon="pi pi-trash" onClick={() => onDelete(row.workAllocationId)} title="Delete" />
          </div>
        )}
        headerStyle={{ textAlign: 'center' }}
        bodyStyle={{ textAlign: 'center' }}
      />
    </DataTable>
  );
}
