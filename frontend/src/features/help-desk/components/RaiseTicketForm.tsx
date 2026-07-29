import { Button } from 'primereact/button';
import { AppTextarea, AppSelect } from '../../../shared/components/forms';
import { useLookupOptions } from '../../../shared/hooks/useMasters';
import { useTicketForm } from './form.hook';

interface RaiseTicketFormProps {
  onSubmit?: () => void;
}

export default function RaiseTicketForm({ onSubmit }: RaiseTicketFormProps) {
  const { formData, updateField, submit, isSubmitting, reset } = useTicketForm();
  const categoryOptions = useLookupOptions('TicketCategory');
  const priorityOptions = useLookupOptions('Priority');

  const handleSubmit = async () => {
    await submit();
    onSubmit?.();
  };

  return (
    <div className="glass-card fade-in" style={{ padding: 24, maxWidth: 640 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 20 }}>Raise a New Ticket</h3>
      <div className="form-grid" style={{ maxWidth: 480 }}>
        <div className="form-field">
          <label>Issue Category</label>
          <AppSelect
            value={formData.issueCategory}
            onChange={(val) => updateField('issueCategory', val)}
            options={categoryOptions}
            placeholder="Select category"
          />
        </div>
        <div className="form-field">
          <label>Priority</label>
          <AppSelect
            value={formData.priority}
            onChange={(val) => updateField('priority', val as 'High' | 'Medium' | 'Low')}
            options={priorityOptions}
          />
        </div>
        <div className="form-field full-width">
          <label>Description</label>
          <AppTextarea
            value={formData.issueDescription}
            onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => updateField('issueDescription', e.target.value)}
            placeholder="Provide detailed information about your issue..."
            rows={4}
          />
        </div>
      </div>
      <div style={{ display: 'flex', gap: 12, justifyContent: 'flex-end', marginTop: 16 }}>
        <Button label="Cancel" className="btn btn-secondary" onClick={reset} />
        <Button
          label="Submit Ticket"
          className="btn btn-primary"
          onClick={handleSubmit}
          disabled={!formData.issueCategory || !formData.issueDescription.trim() || isSubmitting}
          loading={isSubmitting}
        />
      </div>
    </div>
  );
}
