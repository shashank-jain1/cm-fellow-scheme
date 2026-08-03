import { Button } from 'primereact/button';
import WorkAllocationBasicFields from './WorkAllocationBasicFields';
import WorkAllocationSurveyFields from './WorkAllocationSurveyFields';
import WorkAllocationLocationFields from './WorkAllocationLocationFields';
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
  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 20 }}>
      <WorkAllocationBasicFields formData={formData} errors={errors} onChange={onChange} />
      <WorkAllocationSurveyFields formData={formData} errors={errors} onChange={onChange} />
      <WorkAllocationLocationFields formData={formData} errors={errors} onChange={onChange} />
      <div style={{ display: 'flex', gap: 12, justifyContent: 'flex-end', marginTop: 16 }}>
        <Button label="Cancel" onClick={onCancel} className="btn btn-secondary" disabled={isLoading} />
        <Button label={isEditing ? 'Update' : 'Create'} onClick={onSubmit} className="btn btn-primary" loading={isLoading} />
      </div>
    </div>
  );
}
