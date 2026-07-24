import { Button } from 'primereact/button';
import { InputTextarea } from 'primereact/inputtextarea';
import FormSelect from '../../../shared/components/FormSelect';
import { useApplyLeaveForm } from './form.hook';

const leaveTypeOptions = [
  { label: 'Casual Leave', value: 'casual' },
  { label: 'Sick Leave', value: 'sick' },
  { label: 'Earned Leave', value: 'earned' },
  { label: 'Maternity Leave', value: 'maternity' },
  { label: 'Paternity Leave', value: 'paternity' },
  { label: 'Unpaid Leave', value: 'unpaid' },
];

interface ApplyLeaveFormProps {
  onSuccess?: () => void;
}

export default function ApplyLeaveForm({ onSuccess }: ApplyLeaveFormProps) {
  const { formData, errors, handleChange, handleSubmit, isSubmitting } = useApplyLeaveForm(onSuccess);

  return (
    <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: 20 }}>
      <div>
        <label style={{ display: 'block', marginBottom: 6, fontSize: 13, fontWeight: 500, color: 'var(--text-secondary)' }}>
          Leave Type
        </label>
        <FormSelect
          value={formData.leaveType}
          onChange={(val: string) => handleChange('leaveType', val)}
          options={leaveTypeOptions}
          placeholder="Select leave type"
        />
        {errors.leaveType && <span style={{ color: 'var(--red-600)', fontSize: 12 }}>{errors.leaveType}</span>}
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 16 }}>
        <div>
          <label style={{ display: 'block', marginBottom: 6, fontSize: 13, fontWeight: 500, color: 'var(--text-secondary)' }}>
            From Date
          </label>
          <input
            type="date"
            value={formData.fromDate}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => handleChange('fromDate', e.target.value)}
            className="form-input"
          />
          {errors.fromDate && <span style={{ color: 'var(--red-600)', fontSize: 12 }}>{errors.fromDate}</span>}
        </div>
        <div>
          <label style={{ display: 'block', marginBottom: 6, fontSize: 13, fontWeight: 500, color: 'var(--text-secondary)' }}>
            To Date
          </label>
          <input
            type="date"
            value={formData.toDate}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => handleChange('toDate', e.target.value)}
            className="form-input"
          />
          {errors.toDate && <span style={{ color: 'var(--red-600)', fontSize: 12 }}>{errors.toDate}</span>}
        </div>
      </div>

      <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
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

      <div>
        <label style={{ display: 'block', marginBottom: 6, fontSize: 13, fontWeight: 500, color: 'var(--text-secondary)' }}>
          Reason for Leave
        </label>
        <InputTextarea
          value={formData.leaveReason}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => handleChange('leaveReason', e.target.value)}
          rows={4}
          placeholder="Provide a reason for your leave request"
        />
        {errors.leaveReason && <span style={{ color: 'var(--red-600)', fontSize: 12 }}>{errors.leaveReason}</span>}
      </div>

      <div>
        <label style={{ display: 'block', marginBottom: 6, fontSize: 13, fontWeight: 500, color: 'var(--text-secondary)' }}>
          Attachment (optional)
        </label>
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
