import { AppInput, AppTextarea } from '../../../shared/components/forms';
import { AppButton, FormField, FormGrid } from '../../../shared/components/ui';

interface ProfileFormProps {
  form: { name: string; phone: string; email: string; address: string; qualification: string; experience: string };
  handleChange: (field: string, value: string) => void;
  onSubmit: () => void;
  isPending: boolean;
}

export default function ProfileForm({ form, handleChange, onSubmit, isPending }: ProfileFormProps) {
  return (
    <div style={{ padding: 24, background: 'var(--surface-card)', border: '1px solid var(--border-color)', borderRadius: 12 }}>
      <FormGrid columns={2}>
        <FormField label="Full Name" required>
          <AppInput value={form.name} onChange={(e) => handleChange('name', e.target.value)} placeholder="Enter full name" style={{ width: '100%' }} />
        </FormField>
        <FormField label="Phone" required>
          <AppInput value={form.phone} onChange={(e) => handleChange('phone', e.target.value)} placeholder="Enter phone number" style={{ width: '100%' }} />
        </FormField>
        <FormField label="Email" required>
          <AppInput value={form.email} onChange={(e) => handleChange('email', e.target.value)} placeholder="Enter email address" style={{ width: '100%' }} />
        </FormField>
        <FormField label="Qualification">
          <AppInput value={form.qualification} onChange={(e) => handleChange('qualification', e.target.value)} placeholder="e.g. B.Tech, MBA" style={{ width: '100%' }} />
        </FormField>
        <FormField label="Address" fullWidth>
          <AppTextarea value={form.address} onChange={(e) => handleChange('address', e.target.value)} placeholder="Enter full address" rows={3} style={{ width: '100%' }} />
        </FormField>
        <FormField label="Experience" fullWidth>
          <AppTextarea value={form.experience} onChange={(e) => handleChange('experience', e.target.value)} placeholder="Describe your work experience" rows={3} style={{ width: '100%' }} />
        </FormField>
      </FormGrid>
      <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, marginTop: 20 }}>
        <AppButton variant="primary" onClick={onSubmit} loading={isPending} disabled={isPending}>Save Changes</AppButton>
      </div>
    </div>
  );
}
