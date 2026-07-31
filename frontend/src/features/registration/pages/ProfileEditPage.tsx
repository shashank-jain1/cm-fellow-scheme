import { useState, useEffect, useRef } from 'react';
import { Toast } from 'primereact/toast';
import { useAuth } from '../../auth/useAuth';
import { useFellow, useUpdateProfileMutation } from '../queries';
import { PageHeader } from '../../../shared/components/ui';
import ProfileForm from '../components/ProfileForm';

export default function ProfileEditPage() {
  const { user } = useAuth();
  const toast = useRef<Toast>(null);
  const { data: profile, isLoading } = useFellow(user?.userAccountId ?? 0);
  const updateProfile = useUpdateProfileMutation();

  const [form, setForm] = useState({ name: '', phone: '', email: '', address: '', qualification: '', experience: '' });

  useEffect(() => {
    if (profile) {
      setForm({
        name: `${profile.firstName} ${profile.lastName}`,
        phone: profile.phone ?? '',
        email: profile.email ?? '',
        address: (profile as any).address ?? '',
        qualification: (profile as any).qualification ?? '',
        experience: (profile as any).experience ?? '',
      });
    }
  }, [profile]);

  const handleChange = (field: string, value: string) => setForm((prev) => ({ ...prev, [field]: value }));

  const handleSubmit = async () => {
    await updateProfile.mutateAsync({ applicantId: user?.userAccountId ?? 0, data: form });
    toast.current?.show({ severity: 'success', summary: 'Updated', detail: 'Profile updated successfully' });
  };

  return (
    <div>
      <Toast ref={toast} />
      <PageHeader title="Edit Profile" subtitle="Update your personal information" />
      {isLoading ? (
        <div style={{ padding: 40, textAlign: 'center', color: 'var(--text-secondary)' }}>Loading profile...</div>
      ) : (
        <ProfileForm form={form} handleChange={handleChange} onSubmit={handleSubmit} isPending={updateProfile.isPending} />
      )}
    </div>
  );
}
