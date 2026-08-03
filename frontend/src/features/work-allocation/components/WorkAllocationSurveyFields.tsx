import { AppTextarea, AppCalendar } from '../../../shared/components/forms';
import type { WorkAllocationFormData } from '../types';

interface Props {
  formData: WorkAllocationFormData;
  errors: Partial<Record<keyof WorkAllocationFormData, string>>;
  onChange: (field: keyof WorkAllocationFormData, value: string | number) => void;
}

export default function WorkAllocationSurveyFields({ formData, errors, onChange }: Props) {
  return (
    <>
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
    </>
  );
}
