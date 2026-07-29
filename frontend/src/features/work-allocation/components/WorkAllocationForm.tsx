import { Button } from 'primereact/button';
import { AppInputNumber, AppTextarea, AppCalendar, AppSelect } from '../../../shared/components/forms';
import { useLookupOptions, useProjects, useWorks, useDivisions, useDistricts, useBlocks } from '../../../shared/hooks/useMasters';
import type { WorkAllocationFormData } from '../types';

interface WorkAllocationFormProps {
  formData: WorkAllocationFormData;
  errors: Partial<Record<keyof WorkAllocationFormData, string>>;
  onChange: (field: keyof WorkAllocationFormData, value: string | number) => void;
  onSubmit: () => void;
  onCancel: () => void;
  isLoading?: boolean;
  isEditing?: boolean;
}

export default function WorkAllocationForm({
  formData,
  errors,
  onChange,
  onSubmit,
  onCancel,
  isLoading = false,
  isEditing = false,
}: WorkAllocationFormProps) {
  const priorityOptions = useLookupOptions('Priority');

  const { data: projects } = useProjects();
  const projectOptions = (projects ?? []).map((p) => ({ label: p.projectName, value: String(p.projectId) }));

  const { data: works } = useWorks(formData.projectId || null);
  const workOptions = (works ?? []).map((w) => ({ label: w.workName, value: String(w.workId) }));

  const { data: divisions } = useDivisions(null);
  const divisionOptions = (divisions ?? []).map((d) => ({ label: d.divisionName, value: String(d.divisionId) }));

  const { data: districts } = useDistricts(formData.divisionId || null);
  const districtOptions = (districts ?? []).map((d) => ({ label: d.districtName, value: String(d.districtId) }));

  const { data: blocks } = useBlocks(formData.districtId || null);
  const blockOptions = (blocks ?? []).map((b) => ({ label: b.blockName, value: String(b.blockId) }));

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 20 }}>
      <div className="form-grid">
        <div className="form-field">
          <label>Project <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <AppSelect
            value={formData.projectId ? String(formData.projectId) : ''}
            options={projectOptions}
            onChange={(val) => {
              onChange('projectId', Number(val));
              onChange('workProjectId', 0);
            }}
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

      <div className="form-field full-width">
        <label>Work Description <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
        <AppTextarea
          value={formData.workDescription}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onChange('workDescription', e.target.value)}
          rows={4}
          maxLength={1000}
          placeholder="Describe the scope and objectives of work"
          className={errors.workDescription ? 'p-invalid' : ''}
        />
        {errors.workDescription && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.workDescription}</small>}
      </div>

      <div className="form-grid">
        <div className="form-field">
          <label>Start Date <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <AppCalendar
            value={formData.startDate ? new Date(formData.startDate) : null}
            onChange={(e) => onChange('startDate', e.value?.toISOString().split('T')[0] ?? '')}
            showOnFocus={false}
            className={errors.startDate ? 'p-invalid' : ''}
          />
          {errors.startDate && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.startDate}</small>}
        </div>

        <div className="form-field">
          <label>End Date <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <AppCalendar
            value={formData.endDate ? new Date(formData.endDate) : null}
            onChange={(e) => onChange('endDate', e.value?.toISOString().split('T')[0] ?? '')}
            showOnFocus={false}
            className={errors.endDate ? 'p-invalid' : ''}
          />
          {errors.endDate && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.endDate}</small>}
        </div>
      </div>

      <div className="form-grid">
        <div className="form-field">
          <label>Division <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <AppSelect
            value={formData.divisionId ? String(formData.divisionId) : ''}
            options={divisionOptions}
            onChange={(val) => {
              onChange('divisionId', Number(val));
              onChange('districtId', 0);
              onChange('blockId', 0);
            }}
            placeholder="Select division"
          />
          {errors.divisionId && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.divisionId}</small>}
        </div>

        <div className="form-field">
          <label>District <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <AppSelect
            value={formData.districtId ? String(formData.districtId) : ''}
            options={districtOptions}
            onChange={(val) => {
              onChange('districtId', Number(val));
              onChange('blockId', 0);
            }}
            placeholder="Select district"
            disabled={!formData.divisionId}
          />
          {errors.districtId && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.districtId}</small>}
        </div>

        <div className="form-field">
          <label>Block <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <AppSelect
            value={formData.blockId ? String(formData.blockId) : ''}
            options={blockOptions}
            onChange={(val) => onChange('blockId', Number(val))}
            placeholder="Select block"
            disabled={!formData.districtId}
          />
          {errors.blockId && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.blockId}</small>}
        </div>
      </div>

      <div style={{ display: 'flex', gap: 12, justifyContent: 'flex-end', marginTop: 16 }}>
        <Button
          label="Cancel"
          onClick={onCancel}
          className="btn btn-secondary"
          disabled={isLoading}
        />
        <Button
          label={isEditing ? 'Update' : 'Create'}
          onClick={onSubmit}
          className="btn btn-primary"
          loading={isLoading}
        />
      </div>
    </div>
  );
}
