import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
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
    <DataTable
      value={plans}
      responsiveLayout="scroll"
      emptyMessage="No improvement plans"
      dataKey="improvementPlanId"
    >
      <Column field="title" header="Title" bodyStyle={{ fontWeight: 600, fontSize: 14, color: 'var(--text-primary)' }} />
      <Column
        field="description"
        header="Description"
        bodyStyle={{ fontSize: 13, color: 'var(--text-secondary)', maxWidth: 300, overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap' }}
      />
      <Column
        header="Start Date"
        body={(row: ImprovementPlanDto) => <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>{new Date(row.startDate).toLocaleDateString()}</span>}
      />
      <Column
        header="End Date"
        body={(row: ImprovementPlanDto) => <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>{new Date(row.endDate).toLocaleDateString()}</span>}
      />
      <Column
        header="Status"
        body={(row: ImprovementPlanDto) => <Tag value={row.status} severity={statusSeverity[row.status]} />}
      />
    </DataTable>
  );
}
