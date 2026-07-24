import { Button } from 'primereact/button';
import { InputTextarea } from 'primereact/inputtextarea';
import FormSelect from '../../../shared/components/FormSelect';
import { useTicketForm } from './form.hook';

const categoryOptions = [
  { label: 'Technical Issue', value: 'Technical Issue' },
  { label: 'Account Access', value: 'Account Access' },
  { label: 'Survey Problem', value: 'Survey Problem' },
  { label: 'Attendance Issue', value: 'Attendance Issue' },
  { label: 'Other', value: 'Other' },
];

const priorityOptions = [
  { label: 'High', value: 'High' },
  { label: 'Medium', value: 'Medium' },
  { label: 'Low', value: 'Low' },
];

interface RaiseTicketFormProps {
  onSubmit?: () => void;
}

export default function RaiseTicketForm({ onSubmit }: RaiseTicketFormProps) {
  const { formData, updateField, submit, isSubmitting, reset } = useTicketForm();

  const handleSubmit = async () => {
    await submit();
    onSubmit?.();
  };

  return (
    <div className="card fade-in" style={{ padding: 24, maxWidth: 640 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 20 }}>Raise a New Ticket</h3>
      <div style={{ display: 'flex', flexDirection: 'column', gap: 16 }}>
        <div className="form-group">
          <label className="form-label">Issue Category</label>
          <FormSelect
            value={formData.issueCategory}
            onChange={(val) => updateField('issueCategory', val)}
            options={categoryOptions}
            placeholder="Select category"
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Priority</label>
          <FormSelect
            value={formData.priority}
            onChange={(val) => updateField('priority', val as 'High' | 'Medium' | 'Low')}
            options={priorityOptions}
            style={{ width: '100%' }}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Description</label>
          <InputTextarea
            value={formData.issueDescription}
            onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => updateField('issueDescription', e.target.value)}
            placeholder="Provide detailed information about your issue..."
            rows={4}
            style={{ width: '100%', resize: 'vertical' }}
          />
        </div>
        <div style={{ display: 'flex', gap: 12, justifyContent: 'flex-end' }}>
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
    </div>
  );
}
