import { InputText } from 'primereact/inputtext';
import FormSelect from '../../../shared/components/FormSelect';
import type { DashboardFilters } from '../types';

interface DashboardFilterBarProps {
  filters: DashboardFilters;
  onChange: (filters: DashboardFilters) => void;
}

const projectOptions = [
  { label: 'All Projects', value: '' },
  { label: 'Project Alpha', value: '1' },
  { label: 'Project Beta', value: '2' },
  { label: 'Project Gamma', value: '3' },
];

export default function DashboardFilterBar({ filters, onChange }: DashboardFilterBarProps) {
  return (
    <div style={{ display: 'flex', gap: 12, marginBottom: 24, flexWrap: 'wrap' }}>
      <div className="form-group" style={{ marginBottom: 0 }}>
        <label className="form-label">Start Date</label>
        <InputText
          type="date"
          value={filters.startDate ?? ''}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
            onChange({ ...filters, startDate: e.target.value || undefined })
          }
        />
      </div>
      <div className="form-group" style={{ marginBottom: 0 }}>
        <label className="form-label">End Date</label>
        <InputText
          type="date"
          value={filters.endDate ?? ''}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
            onChange({ ...filters, endDate: e.target.value || undefined })
          }
        />
      </div>
      <div className="form-group" style={{ marginBottom: 0 }}>
        <label className="form-label">Project</label>
        <FormSelect
          value={filters.projectId?.toString() ?? ''}
          onChange={(val) => onChange({ ...filters, projectId: val ? Number(val) : undefined })}
          options={projectOptions}
          style={{ width: 180 }}
        />
      </div>
    </div>
  );
}
