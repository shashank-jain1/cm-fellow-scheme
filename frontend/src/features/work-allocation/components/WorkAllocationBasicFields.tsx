import { AppSelect, AppInputNumber } from '../../../shared/components/forms';
import { useLookupOptions, useProjects, useWorks } from '../../../shared/hooks/useMasters';
import type { WorkAllocationFormData } from '../types';

interface Props {
  formData: WorkAllocationFormData;
  errors: Partial<Record<keyof WorkAllocationFormData, string>>;
  onChange: (field: keyof WorkAllocationFormData, value: string | number) => void;
}

export default function WorkAllocationBasicFields({ formData, errors, onChange }: Props) {
  const priorityOptions = useLookupOptions('Priority');
  const { data: projects } = useProjects();
  const projectOptions = (projects ?? []).map((p) => ({ label: p.projectName, value: String(p.projectId) }));
  const { data: works } = useWorks(formData.projectId || null);
  const workOptions = (works ?? []).map((w) => ({ label: w.workName, value: String(w.workId) }));

  return (
    <div className="form-grid">
      <div className="form-field">
        <label>Project <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
        <AppSelect
          value={formData.projectId ? String(formData.projectId) : ''}
          options={projectOptions}
          onChange={(val) => { onChange('projectId', Number(val)); onChange('workProjectId', 0); }}
          placeholder="Select project"
        />
        {errors.projectId && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.projectId}</small>}
      </div>
      <div className="form-field">
        <label>Work Project <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
        <AppSelect
          value={formData.workProjectId ? String(formData.workProjectId) : ''}
          options={workOptions}
          onChange={(val) => onChange('workProjectId', Number(val))}
          placeholder="Select work project"
          disabled={!formData.projectId}
        />
        {errors.workProjectId && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.workProjectId}</small>}
      </div>
      <div className="form-field">
        <label>Priority <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
        <AppSelect
          value={formData.priority}
          options={priorityOptions}
          onChange={(val) => onChange('priority', val)}
          placeholder="Select priority"
        />
      </div>
      <div className="form-field">
        <label>Surveys Per Intern <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
        <AppInputNumber
          value={formData.surveysPerIntern || undefined}
          onValueChange={(e) => onChange('surveysPerIntern', e.value ?? 0)}
          className={errors.surveysPerIntern ? 'p-invalid' : ''}
        />
        {errors.surveysPerIntern && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.surveysPerIntern}</small>}
      </div>
    </div>
  );
}
