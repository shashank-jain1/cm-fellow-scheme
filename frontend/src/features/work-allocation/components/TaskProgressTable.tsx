import { Tag } from 'primereact/tag';
import type { TaskProgressDto } from '../types';
import { getProgressColor, getStatusSeverity } from './TaskProgressFilters';

interface TaskProgressTableProps {
  data: TaskProgressDto[];
  onRowClick?: (task: TaskProgressDto) => void;
}

export default function TaskProgressTable({ data, onRowClick }: TaskProgressTableProps) {
  return (
    <div className="table-wrapper">
      <table style={{ width: '100%', borderCollapse: 'collapse' }}>
        <thead>
          <tr style={{ background: 'var(--navy-50)' }}>
            {['Project', 'Work', 'Surveys', 'Pending', 'Completion %', 'Status'].map((h) => (
              <th
                key={h}
                style={{
                  padding: '12px 16px',
                  textAlign: 'left',
                  fontSize: 12,
                  fontWeight: 600,
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
          {data.map((task) => (
            <tr
              key={task.taskProgressId}
              style={{
                borderBottom: '1px solid var(--border-light)',
                cursor: onRowClick ? 'pointer' : 'default',
              }}
              onClick={() => onRowClick?.(task)}
            >
              <td style={{ padding: '14px 16px', fontWeight: 500, fontSize: 14 }}>
                {task.projectName}
              </td>
              <td style={{ padding: '14px 16px', fontSize: 14, color: 'var(--text-secondary)' }}>
                {task.workProject}
              </td>
              <td style={{ padding: '14px 16px', fontSize: 14 }}>
                {task.completedSurveys} / {task.numberOfSurveys}
              </td>
              <td style={{ padding: '14px 16px', fontSize: 14 }}>
                {task.pendingSurveys > 0 ? (
                  <span style={{ color: '#d97706', fontWeight: 600 }}>{task.pendingSurveys}</span>
                ) : (
                  <span style={{ color: '#059669', fontWeight: 600 }}>0</span>
                )}
              </td>
              <td style={{ padding: '14px 16px' }}>
                <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
                  <div style={{ width: 80, height: 6, borderRadius: 3, background: 'var(--navy-100)' }}>
                    <div
                      style={{
                        width: `${task.completionPercentage}%`,
                        height: '100%',
                        borderRadius: 3,
                        background: getProgressColor(task.completionPercentage),
                      }}
                    />
                  </div>
                  <span style={{ fontSize: 12, fontWeight: 500, minWidth: 40 }}>
                    {task.completionPercentage.toFixed(1)}%
                  </span>
                </div>
              </td>
              <td style={{ padding: '14px 16px' }}>
                <Tag
                  value={task.workStatus}
                  severity={getStatusSeverity(task.workStatus)}
                />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
