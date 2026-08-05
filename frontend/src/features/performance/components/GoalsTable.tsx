import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Tag } from 'primereact/tag';
import { AppButton, EmptyState, SkeletonTable } from '../../../shared/components/ui';
import type { PerformanceGoalDto, GoalStatus } from '../types';

const statusSeverity: Record<string, 'success' | 'info' | 'warning' | 'danger' | 'secondary'> = {
  NotStarted: 'secondary',
  InProgress: 'info',
  Completed: 'success',
  Missed: 'danger',
};

const nextStatusMap: Record<GoalStatus, GoalStatus | null> = {
  NotStarted: 'InProgress',
  InProgress: 'Completed',
  Completed: null,
  Missed: null,
};

interface GoalsTableProps {
  goals: PerformanceGoalDto[];
  isLoading: boolean;
  onAdvanceStatus: (goalId: number, currentStatus: GoalStatus) => void;
  isPending: boolean;
}

export default function GoalsTable({ goals, isLoading, onAdvanceStatus, isPending }: GoalsTableProps) {
  if (isLoading) return <SkeletonTable columns={5} />;

  if (goals.length === 0) {
    return <EmptyState icon="pi pi-flag" title="No goals yet" description="Create your first performance goal to get started" />;
  }

  return (
    <DataTable
      value={goals}
      responsiveLayout="scroll"
      emptyMessage="No goals yet"
      rowKey="goalId"
    >
      <Column field="title" header="Title" bodyStyle={{ fontWeight: 600, fontSize: 14, color: 'var(--text-primary)' }} />
      <Column
        field="description"
        header="Description"
        bodyStyle={{ fontSize: 13, color: 'var(--text-secondary)', maxWidth: 300, overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap' }}
      />
      <Column
        header="Target Date"
        body={(row: PerformanceGoalDto) => <span style={{ fontSize: 13, color: 'var(--text-secondary)' }}>{new Date(row.targetDate).toLocaleDateString()}</span>}
      />
      <Column
        header="Status"
        body={(row: PerformanceGoalDto) => <Tag value={row.status} severity={statusSeverity[row.status]} />}
      />
      <Column
        header="Action"
        body={(row: PerformanceGoalDto) => {
          const nextStatus = nextStatusMap[row.status];
          return nextStatus ? (
            <AppButton variant="accent" size="sm" onClick={() => onAdvanceStatus(row.goalId, row.status)} loading={isPending}>
              Move to {nextStatus}
            </AppButton>
          ) : null;
        }}
      />
    </DataTable>
  );
}
