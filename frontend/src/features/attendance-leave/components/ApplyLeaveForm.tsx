import { AppTextarea, AppCalendar, AppSelect, AppSwitch } from '../../../shared/components/forms';
import AppButton from '../../../shared/components/ui/AppButton';
import { useLookupOptions } from '../../../shared/hooks/useMasters';
import { useApplyLeaveForm } from './form.hook';

interface ApplyLeaveFormProps {
  onSuccess?: () => void;
}

export default function ApplyLeaveForm({ onSuccess }: ApplyLeaveFormProps) {
  const { formData, errors, handleChange, handleSubmit, isSubmitting } = useApplyLeaveForm(onSuccess);
  const leaveTypeOptions = useLookupOptions('LeaveType');

  return (
    <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: 20 }}>
      <div className="form-grid">
        <div className="form-field">
          <label>Leave Type *</label>
          <AppSelect
            value={formData.leaveTypeId !== null ? String(formData.leaveTypeId) : ''}
            onChange={(val: string) => handleChange('leaveTypeId', val ? Number(val) : null)}
            options={leaveTypeOptions}
            placeholder="Select leave type"
          />
          {errors.leaveTypeId && <small style={{ color: 'var(--red-600)', fontSize: 12 }}>{errors.leaveTypeId}</small>}
        </div>

        <div className="form-field">
          <label>From Date *</label>
          <AppCalendar
            value={formData.fromDate ? new Date(formData.fromDate) : null}
            onChange={(e) => handleChange('fromDate', e.value ? e.value.toISOString().split('T')[0] : '')}
            dateFormat="dd/mm/yy"
            showIcon
          />
          {errors.fromDate && <small style={{ color: 'var(--red-600)', fontSize: 12 }}>{errors.fromDate}</small>}
        </div>

        <div className="form-field">
          <label>To Date *</label>
          <AppCalendar
            value={formData.toDate ? new Date(formData.toDate) : null}
            onChange={(e) => handleChange('toDate', e.value ? e.value.toISOString().split('T')[0] : '')}
            dateFormat="dd/mm/yy"
            showIcon
          />
          {errors.toDate && <small style={{ color: 'var(--red-600)', fontSize: 12 }}>{errors.toDate}</small>}
        </div>
      </div>

      <div className="form-field" style={{ maxWidth: 200 }}>
        <label>Half Day</label>
        <div style={{ display: 'flex', alignItems: 'center', gap: 8, height: 40 }}>
          <AppSwitch
            checked={formData.isHalfDay}
            onChange={(e) => handleChange('isHalfDay', e.value ?? e.target?.checked ?? false)}
          />
          <label style={{ fontSize: 13, color: 'var(--text-secondary)' }}>
            Half Day
          </label>
        </div>
      </div>

      <div className="form-field full-width">
        <label>Reason for Leave *</label>
        <AppTextarea
          value={formData.reason}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => handleChange('reason', e.target.value)}
          rows={4}
          placeholder="Provide a reason for your leave request"
        />
        {errors.reason && <small style={{ color: 'var(--red-600)', fontSize: 12 }}>{errors.reason}</small>}
      </div>

      <AppButton
        type="submit"
        loading={isSubmitting}
        disabled={isSubmitting}
        icon="pi pi-send"
        style={{ alignSelf: 'flex-start' }}
      >
        Submit Leave Application
      </AppButton>
    </form>
  );
}
