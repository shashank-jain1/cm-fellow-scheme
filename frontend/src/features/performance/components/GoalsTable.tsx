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
    <table style={{ width: '100%', borderCollapse: 'collapse' }}>
      <thead>
        <tr>
          {['Title', 'Description', 'Target Date', 'Status', 'Action'].map((h) => (
            <th
              key={h}
              style={{
                padding: '12px 16px', textAlign: 'left', fontSize: 12, fontWeight: 700,
                color: 'var(--text-secondary)', textTransform: 'uppercase', letterSpacing: '0.5px',
                borderBottom: '1px solid var(--border-color)',
              }}
            >
              {h}
            </th>
          ))}
        </tr>
      </thead>
      <tbody>
        {goals.map((g) => {
          const nextStatus = nextStatusMap[g.status];
          return (
            <tr key={g.goalId} style={{ borderBottom: '1px solid var(--border-light)' }}>
              <td style={{ padding: '14px 16px', fontWeight: 600, fontSize: 14, color: 'var(--text-primary)' }}>{g.title}</td>
              <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-secondary)', maxWidth: 300, overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap' }}>{g.description}</td>
              <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-secondary)' }}>{new Date(g.targetDate).toLocaleDateString()}</td>
              <td style={{ padding: '14px 16px' }}><Tag value={g.status} severity={statusSeverity[g.status]} /></td>
              <td style={{ padding: '14px 16px' }}>
                {nextStatus && (
                  <AppButton variant="accent" size="sm" onClick={() => onAdvanceStatus(g.goalId, g.status)} loading={isPending}>
                    Move to {nextStatus}
                  </AppButton>
                )}
              </td>
            </tr>
          );
        })}
      </tbody>
    </table>
  );
}
