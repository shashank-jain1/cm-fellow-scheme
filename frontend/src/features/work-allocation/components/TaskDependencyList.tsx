import { AppButton } from '../../../shared/components/ui';
import type { TaskDependency } from '../types';

interface TaskDependencyListProps {
  dependencies: TaskDependency[];
  isLoading: boolean;
}

export default function TaskDependencyList({ dependencies, isLoading }: TaskDependencyListProps) {
  if (isLoading) {
    return (
      <div style={{ padding: 12 }}>
        {[1, 2, 3].map((n) => (
          <div key={n} style={{ display: 'flex', gap: 12, padding: '10px 0', borderBottom: '1px solid var(--border-light)' }}>
            <div className="skeleton" style={{ width: '30%', height: 14 }} />
            <div className="skeleton" style={{ width: '30%', height: 14 }} />
            <div className="skeleton" style={{ width: '20%', height: 14 }} />
          </div>
        ))}
      </div>
    );
  }

  if (dependencies.length === 0) {
    return (
      <div style={{ padding: 24, textAlign: 'center', color: 'var(--text-muted)', fontSize: 14 }}>
        No dependencies defined for this work allocation
      </div>
    );
  }

  return (
    <div style={{ display: 'flex', flexDirection: 'column' }}>
      {dependencies.map((dep) => (
        <div
          key={dep.dependencyId}
          style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', padding: '12px 16px', borderBottom: '1px solid var(--border-light)' }}
        >
          <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
            <i className="pi pi-arrow-right" style={{ color: 'var(--text-muted)', fontSize: 14 }} />
            <div>
              <div style={{ fontSize: 14, fontWeight: 500 }}>Task #{dep.taskProgressId}</div>
              <div style={{ fontSize: 12, color: 'var(--text-muted)' }}>
                Requires: {dep.prerequisiteTaskName || `Task #${dep.prerequisiteTaskProgressId}`}
              </div>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
}
