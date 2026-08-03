import type { TaskProgressDto } from '../types';
import TaskProgressTable from './TaskProgressTable';

interface TaskProgressGridProps {
  data: TaskProgressDto[];
  isLoading?: boolean;
  onRowClick?: (task: TaskProgressDto) => void;
}

export default function TaskProgressGrid({ data, isLoading = false, onRowClick }: TaskProgressGridProps) {
  if (isLoading) {
    return (
      <div style={{ padding: 20 }}>
        {[1, 2, 3, 4, 5].map((n) => (
          <div key={n} style={{ display: 'flex', gap: 16, padding: '14px 0', borderBottom: '1px solid var(--border-light)' }}>
            <div className="skeleton" style={{ width: '20%', height: 14 }} />
            <div className="skeleton" style={{ width: '25%', height: 14 }} />
            <div className="skeleton" style={{ width: '20%', height: 14 }} />
            <div className="skeleton" style={{ width: '15%', height: 14 }} />
            <div className="skeleton" style={{ width: '10%', height: 14 }} />
          </div>
        ))}
      </div>
    );
  }

  if (data.length === 0) {
    return (
      <div className="empty-state">
        <i className="pi pi-chart-bar" />
        <h3>No task progress data</h3>
        <p>Task progress information will appear here</p>
      </div>
    );
  }

  return <TaskProgressTable data={data} onRowClick={onRowClick} />;
}
