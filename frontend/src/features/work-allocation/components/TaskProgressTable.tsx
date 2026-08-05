import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
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
      <DataTable
        value={data}
        responsiveLayout="scroll"
        emptyMessage="No task progress data"
        dataKey="taskProgressId"
        onRowClick={(e) => onRowClick?.(e.data as TaskProgressDto)}
        style={{ cursor: onRowClick ? 'pointer' : 'default' }}
      >
        <Column field="projectName" header="Project" bodyStyle={{ fontWeight: 500, fontSize: 14 }} />
        <Column field="workProject" header="Work" bodyStyle={{ fontSize: 14, color: 'var(--text-secondary)' }} />
        <Column
          header="Surveys"
          body={(row: TaskProgressDto) => <span>{row.completedSurveys} / {row.numberOfSurveys}</span>}
        />
        <Column
          header="Pending"
          body={(row: TaskProgressDto) =>
            row.pendingSurveys > 0
              ? <span style={{ color: '#d97706', fontWeight: 600 }}>{row.pendingSurveys}</span>
              : <span style={{ color: '#059669', fontWeight: 600 }}>0</span>
          }
        />
        <Column
          header="Completion %"
          body={(row: TaskProgressDto) => (
            <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
              <div style={{ width: 80, height: 6, borderRadius: 3, background: 'var(--navy-100)' }}>
                <div
                  style={{
                    width: `${row.completionPercentage}%`,
                    height: '100%',
                    borderRadius: 3,
                    background: getProgressColor(row.completionPercentage),
                  }}
                />
              </div>
              <span style={{ fontSize: 12, fontWeight: 500, minWidth: 40 }}>
                {row.completionPercentage.toFixed(1)}%
              </span>
            </div>
          )}
        />
        <Column
          field="workStatus"
          header="Status"
          body={(row: TaskProgressDto) => (
            <Tag value={row.workStatus} severity={getStatusSeverity(row.workStatus)} />
          )}
        />
      </DataTable>
    </div>
  );
}
