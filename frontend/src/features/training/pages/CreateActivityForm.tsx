import { useNavigate } from 'react-router-dom';
import { useActivityForm } from '../components/form.hook';
import { AppButton } from '../../../shared/components/ui';
import ActivityBasicFields from './ActivityBasicFields';
import ActivityDetailsFields from './ActivityDetailsFields';

export default function CreateActivityForm() {
  const navigate = useNavigate();
  const { formData, updateField, submit, isSubmitting } = useActivityForm();

  const handleSubmit = async () => {
    await submit();
    navigate('/training');
  };

  return (
    <div>
      <ActivityBasicFields
        formData={formData}
        updateField={updateField}
        onNavigate={navigate}
      />
      <div className="card" style={{ padding: 28 }}>
        <ActivityDetailsFields
          activityType={formData.activityType}
          formData={formData as unknown as Record<string, unknown>}
          updateField={updateField}
        />
        <div style={{ display: 'flex', justifyContent: 'flex-end', marginTop: 16 }}>
          <AppButton icon="pi pi-check" loading={isSubmitting} onClick={handleSubmit}>
            Save
          </AppButton>
        </div>
      </div>
    </div>
  );
}
