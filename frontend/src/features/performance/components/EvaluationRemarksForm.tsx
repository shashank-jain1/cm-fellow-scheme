import { Button } from 'primereact/button';
import { AppTextarea } from '../../../shared/components/forms';

interface EvaluationRemarksFormProps {
  value: string;
  onChange: (remarks: string) => void;
  onSubmit: () => void;
  isSubmitting?: boolean;
  initialRemarks?: string | null;
}

export default function EvaluationRemarksForm({
  value,
  onChange,
  onSubmit,
  isSubmitting,
  initialRemarks,
}: EvaluationRemarksFormProps) {
  return (
    <div className="card" style={{ padding: 24 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Evaluation Remarks</h3>
      {initialRemarks && (
        <div style={{ marginBottom: 16, padding: 14, background: 'var(--navy-50)', borderRadius: 'var(--radius-md)' }}>
          <div style={{ fontSize: 11, fontWeight: 600, color: 'var(--text-muted)', textTransform: 'uppercase', letterSpacing: '0.5px', marginBottom: 6 }}>
            Previous Remarks
          </div>
          <div style={{ fontSize: 13, color: 'var(--text-secondary)' }}>{initialRemarks}</div>
        </div>
      )}
      <div className="form-group">
        <label className="form-label">Add / Update Remarks</label>
        <AppTextarea
          value={value}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onChange(e.target.value)}
          placeholder="Enter evaluation remarks..."
          rows={4}
          style={{ width: '100%', resize: 'vertical' }}
        />
      </div>
      <div style={{ display: 'flex', justifyContent: 'flex-end', marginTop: 12 }}>
        <Button
          label="Save Remarks"
          className="btn btn-primary"
          onClick={onSubmit}
          disabled={!value.trim() || isSubmitting}
          loading={isSubmitting}
        />
      </div>
    </div>
  );
}
