import { InputText } from 'primereact/inputtext';
import { InputNumber } from 'primereact/inputnumber';
import { Calendar } from 'primereact/calendar';
import { Dropdown } from 'primereact/dropdown';
import { Button } from 'primereact/button';
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
  const inputStyle = { width: '100%' };

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 20 }}>
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))', gap: 16 }}>
        <div className="form-group">
          <label style={{ display: 'block', marginBottom: 6, fontWeight: 500, fontSize: 14 }}>
            Project <span style={{ color: 'var(--red-500)' }}>*</span>
          </label>
          <InputNumber
            value={formData.projectId || undefined}
            onValueChange={(e) => onChange('projectId', e.value ?? 0)}
            style={inputStyle}
            className={errors.projectId ? 'p-invalid' : ''}
          />
          {errors.projectId && <small style={{ color: 'var(--red-500)' }}>{errors.projectId}</small>}
        </div>

        <div className="form-group">
          <label style={{ display: 'block', marginBottom: 6, fontWeight: 500, fontSize: 14 }}>
            Work Project <span style={{ color: 'var(--red-500)' }}>*</span>
          </label>
          <InputNumber
            value={formData.workProjectId || undefined}
            onValueChange={(e) => onChange('workProjectId', e.value ?? 0)}
            style={inputStyle}
            className={errors.workProjectId ? 'p-invalid' : ''}
          />
          {errors.workProjectId && <small style={{ color: 'var(--red-500)' }}>{errors.workProjectId}</small>}
        </div>

        <div className="form-group">
          <label style={{ display: 'block', marginBottom: 6, fontWeight: 500, fontSize: 14 }}>
            Priority <span style={{ color: 'var(--red-500)' }}>*</span>
          </label>
          <Dropdown
            value={formData.priority}
            options={priorityOptions}
            onChange={(e) => onChange('priority', e.value)}
            style={inputStyle}
          />
        </div>

        <div className="form-group">
          <label style={{ display: 'block', marginBottom: 6, fontWeight: 500, fontSize: 14 }}>
            Surveys Per Intern <span style={{ color: 'var(--red-500)' }}>*</span>
          </label>
          <InputNumber
            value={formData.surveysPerIntern || undefined}
            onValueChange={(e) => onChange('surveysPerIntern', e.value ?? 0)}
            style={inputStyle}
            className={errors.surveysPerIntern ? 'p-invalid' : ''}
          />
          {errors.surveysPerIntern && <small style={{ color: 'var(--red-500)' }}>{errors.surveysPerIntern}</small>}
        </div>
      </div>

      <div className="form-group">
        <label style={{ display: 'block', marginBottom: 6, fontWeight: 500, fontSize: 14 }}>
          Work Description <span style={{ color: 'var(--red-500)' }}>*</span>
        </label>
        <InputText
          value={formData.workDescription}
          onChange={(e) => onChange('workDescription', e.target.value)}
          style={inputStyle}
          className={errors.workDescription ? 'p-invalid' : ''}
        />
        {errors.workDescription && <small style={{ color: 'var(--red-500)' }}>{errors.workDescription}</small>}
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))', gap: 16 }}>
        <div className="form-group">
          <label style={{ display: 'block', marginBottom: 6, fontWeight: 500, fontSize: 14 }}>
            Start Date <span style={{ color: 'var(--red-500)' }}>*</span>
          </label>
          <Calendar
            value={formData.startDate ? new Date(formData.startDate) : null}
            onChange={(e) => onChange('startDate', e.value?.toISOString().split('T')[0] ?? '')}
            style={inputStyle}
            className={errors.startDate ? 'p-invalid' : ''}
          />
          {errors.startDate && <small style={{ color: 'var(--red-500)' }}>{errors.startDate}</small>}
        </div>

        <div className="form-group">
          <label style={{ display: 'block', marginBottom: 6, fontWeight: 500, fontSize: 14 }}>
            End Date <span style={{ color: 'var(--red-500)' }}>*</span>
          </label>
          <Calendar
            value={formData.endDate ? new Date(formData.endDate) : null}
            onChange={(e) => onChange('endDate', e.value?.toISOString().split('T')[0] ?? '')}
            style={inputStyle}
            className={errors.endDate ? 'p-invalid' : ''}
          />
          {errors.endDate && <small style={{ color: 'var(--red-500)' }}>{errors.endDate}</small>}
        </div>
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))', gap: 16 }}>
        <div className="form-group">
          <label style={{ display: 'block', marginBottom: 6, fontWeight: 500, fontSize: 14 }}>
            Division <span style={{ color: 'var(--red-500)' }}>*</span>
          </label>
          <InputNumber
            value={formData.divisionId || undefined}
            onValueChange={(e) => onChange('divisionId', e.value ?? 0)}
            style={inputStyle}
            className={errors.divisionId ? 'p-invalid' : ''}
          />
          {errors.divisionId && <small style={{ color: 'var(--red-500)' }}>{errors.divisionId}</small>}
        </div>

        <div className="form-group">
          <label style={{ display: 'block', marginBottom: 6, fontWeight: 500, fontSize: 14 }}>
            District <span style={{ color: 'var(--red-500)' }}>*</span>
          </label>
          <InputNumber
            value={formData.districtId || undefined}
            onValueChange={(e) => onChange('districtId', e.value ?? 0)}
            style={inputStyle}
            className={errors.districtId ? 'p-invalid' : ''}
          />
          {errors.districtId && <small style={{ color: 'var(--red-500)' }}>{errors.districtId}</small>}
        </div>

        <div className="form-group">
          <label style={{ display: 'block', marginBottom: 6, fontWeight: 500, fontSize: 14 }}>
            Block <span style={{ color: 'var(--red-500)' }}>*</span>
          </label>
          <InputNumber
            value={formData.blockId || undefined}
            onValueChange={(e) => onChange('blockId', e.value ?? 0)}
            style={inputStyle}
            className={errors.blockId ? 'p-invalid' : ''}
          />
          {errors.blockId && <small style={{ color: 'var(--red-500)' }}>{errors.blockId}</small>}
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
