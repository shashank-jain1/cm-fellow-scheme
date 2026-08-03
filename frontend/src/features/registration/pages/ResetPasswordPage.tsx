import { useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import { Toast } from 'primereact/toast';
import { useResetPasswordMutation } from '../queries';
import { PageHeader } from '../../../shared/components/ui';
import ResetPasswordForm from './ResetPasswordForm';

export default function ResetPasswordPage() {
  const toast = useRef<Toast>(null);
  const navigate = useNavigate();
  const resetMutation = useResetPasswordMutation();

  const handleSubmit = async (email: string, newPassword: string) => {
    await resetMutation.mutateAsync({ email, newPassword });
    toast.current?.show({
      severity: 'success',
      summary: 'Password Reset',
      detail: 'Your password has been reset successfully.',
    });
    setTimeout(() => navigate('/login'), 2000);
  };

  return (
    <div style={{ maxWidth: 480, margin: '60px auto', padding: '0 20px' }}>
      <Toast ref={toast} />
      <PageHeader title="Reset Password" subtitle="Set your new password" />
      <div className="card" style={{ padding: 32 }}>
        <ResetPasswordForm onSubmit={handleSubmit} isPending={resetMutation.isPending} />
      </div>
    </div>
  );
}
