import { InputText } from 'primereact/inputtext';
import { useQuery } from '@tanstack/react-query';
import FormSelect from '../../../shared/components/FormSelect';
import ApiService from '../../../services/ApiService';
import type { DashboardFilters } from '../types';

interface DashboardFilterBarProps {
  filters: DashboardFilters;
  onChange: (filters: DashboardFilters) => void;
}

interface ProjectOption {
  projectId: number;
  projectName: string;
}

export default function DashboardFilterBar({ filters, onChange }: DashboardFilterBarProps) {
  const { data: projects } = useQuery<ProjectOption[]>({
    queryKey: ['dashboard-projects'],
    queryFn: async () => {
      const res = await ApiService.get<ProjectOption[]>('masters/projects');
      return res.data ?? [];
    },
  });

  const projectOptions = [
    { label: 'All Projects', value: '' },
    ...(projects ?? []).map((p) => ({ label: p.projectName, value: String(p.projectId) })),
  ];

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
