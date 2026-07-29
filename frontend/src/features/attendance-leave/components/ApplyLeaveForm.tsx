import { Button } from 'primereact/button';
import { AppTextarea, AppCalendar, AppSelect } from '../../../shared/components/forms';
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
          <label>Leave Type</label>
          <AppSelect
            value={formData.leaveType}
            onChange={(val: string) => handleChange('leaveType', val)}
            options={leaveTypeOptions}
            placeholder="Select leave type"
          />
          {errors.leaveType && <small style={{ color: 'var(--red-600)', fontSize: 12 }}>{errors.leaveType}</small>}
        </div>

        <div className="form-field">
          <label>Reporting Manager *</label>
          <input
            type="text"
            className="form-input"
            value={formData.reportingManagerName}
            onChange={(e) => handleChange('reportingManagerName', e.target.value)}
            placeholder="Enter reporting manager name"
          />
          {errors.reportingManagerName && <small style={{ color: 'var(--red-600)', fontSize: 12 }}>{errors.reportingManagerName}</small>}
        </div>

        <div className="form-field">
          <label>From Date</label>
          <AppCalendar
            value={formData.fromDate ? new Date(formData.fromDate) : null}
            onChange={(e) => handleChange('fromDate', e.value ? e.value.toISOString().split('T')[0] : '')}
            dateFormat="dd/mm/yy"
            showIcon
          />
          {errors.fromDate && <small style={{ color: 'var(--red-600)', fontSize: 12 }}>{errors.fromDate}</small>}
        </div>

        <div className="form-field">
          <label>To Date</label>
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
          <input
            type="checkbox"
            id="halfDayFullDay"
            checked={formData.halfDayFullDay}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => handleChange('halfDayFullDay', e.target.checked)}
          />
          <label htmlFor="halfDayFullDay" style={{ fontSize: 13, color: 'var(--text-secondary)' }}>
            Half Day
          </label>
        </div>
      </div>

      <div className="form-field full-width">
        <label>Reason for Leave</label>
        <AppTextarea
          value={formData.leaveReason}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => handleChange('leaveReason', e.target.value)}
          rows={4}
          placeholder="Provide a reason for your leave request"
        />
        {errors.leaveReason && <small style={{ color: 'var(--red-600)', fontSize: 12 }}>{errors.leaveReason}</small>}
      </div>

      <div className="form-field">
        <label>Attachment (optional)</label>
        <input
          type="file"
          onChange={(e: React.ChangeEvent<HTMLInputElement>) => handleChange('attachmentFile', e.target.files?.[0])}
          accept=".pdf,.jpg,.jpeg,.png"
        />
      </div>

      <Button
        type="submit"
        label="Submit Leave Application"
        icon="pi pi-send"
        loading={isSubmitting}
        disabled={isSubmitting}
        className="btn btn-primary"
        style={{ alignSelf: 'flex-start' }}
      />
    </form>
  );
}
