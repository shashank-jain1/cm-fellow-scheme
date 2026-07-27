import { InputText } from 'primereact/inputtext';
import { InputNumber } from 'primereact/inputnumber';
import { Calendar } from 'primereact/calendar';
import { Button } from 'primereact/button';
import FormSelect from '../../../shared/components/FormSelect';
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

const priorityOptions = [
  { label: 'High', value: 'High' },
  { label: 'Medium', value: 'Medium' },
  { label: 'Low', value: 'Low' },
];

export default function WorkAllocationForm({
  formData,
  errors,
  onChange,
  onSubmit,
  onCancel,
  isLoading = false,
  isEditing = false,
}: WorkAllocationFormProps) {
  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 20 }}>
      <div className="form-grid">
        <div className="form-field">
          <label>Project <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <InputNumber
            value={formData.projectId || undefined}
            onValueChange={(e) => onChange('projectId', e.value ?? 0)}
            className={errors.projectId ? 'p-invalid' : ''}
          />
          {errors.projectId && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.projectId}</small>}
        </div>

        <div className="form-field">
          <label>Work Project <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <InputNumber
            value={formData.workProjectId || undefined}
            onValueChange={(e) => onChange('workProjectId', e.value ?? 0)}
            className={errors.workProjectId ? 'p-invalid' : ''}
          />
          {errors.workProjectId && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.workProjectId}</small>}
        </div>

        <div className="form-field">
          <label>Priority <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <FormSelect
            value={formData.priority}
            options={priorityOptions}
            onChange={(val) => onChange('priority', val)}
          />
        </div>

        <div className="form-field">
          <label>Surveys Per Intern <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <InputNumber
            value={formData.surveysPerIntern || undefined}
            onValueChange={(e) => onChange('surveysPerIntern', e.value ?? 0)}
            className={errors.surveysPerIntern ? 'p-invalid' : ''}
          />
          {errors.surveysPerIntern && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.surveysPerIntern}</small>}
        </div>
      </div>

      <div className="form-field full-width">
        <label>Work Description <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
        <InputText
          value={formData.workDescription}
          onChange={(e) => onChange('workDescription', e.target.value)}
          className={errors.workDescription ? 'p-invalid' : ''}
        />
        {errors.workDescription && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.workDescription}</small>}
      </div>

      <div className="form-grid">
        <div className="form-field">
          <label>Start Date <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <Calendar
            value={formData.startDate ? new Date(formData.startDate) : null}
            onChange={(e) => onChange('startDate', e.value?.toISOString().split('T')[0] ?? '')}
            showOnFocus={false}
            className={errors.startDate ? 'p-invalid' : ''}
          />
          {errors.startDate && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.startDate}</small>}
        </div>

        <div className="form-field">
          <label>End Date <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <Calendar
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
          <label>Division ID <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <InputNumber
            value={formData.divisionId || undefined}
            onValueChange={(e) => onChange('divisionId', e.value ?? 0)}
            className={errors.divisionId ? 'p-invalid' : ''}
          />
          {errors.divisionId && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.divisionId}</small>}
        </div>

        <div className="form-field">
          <label>District ID <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <InputNumber
            value={formData.districtId || undefined}
            onValueChange={(e) => onChange('districtId', e.value ?? 0)}
            className={errors.districtId ? 'p-invalid' : ''}
          />
          {errors.districtId && <small style={{ color: 'var(--badge-red-text)', marginTop: 4, display: 'block' }}>{errors.districtId}</small>}
        </div>

        <div className="form-field">
          <label>Block ID <span style={{ color: 'var(--badge-red-text)' }}>*</span></label>
          <InputNumber
            value={formData.blockId || undefined}
            onValueChange={(e) => onChange('blockId', e.value ?? 0)}
            className={errors.blockId ? 'p-invalid' : ''}
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
