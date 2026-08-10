import { useRef } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { Toast } from 'primereact/toast';
import { useResetPasswordMutation } from '../queries';
import { PageHeader, AppButton } from '../../../shared/components/ui';
import ResetPasswordForm from './ResetPasswordForm';

export default function ResetPasswordPage() {
  const toast = useRef<Toast>(null);
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const resetMutation = useResetPasswordMutation();

  // The token arrives in the emailed reset link (/reset-password?token=...).
  const token = searchParams.get('token') ?? '';

  const handleSubmit = async (newPassword: string) => {
    await resetMutation.mutateAsync({ token, newPassword });
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
        {token ? (
          <ResetPasswordForm onSubmit={handleSubmit} isPending={resetMutation.isPending} />
        ) : (
          <div>
            <p style={{ margin: 0, color: 'var(--text-secondary)', fontSize: 14 }}>
              This page needs the reset link sent to your email. Open the link from that
              email, or request a new one.
            </p>
            <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, marginTop: 24 }}>
              <AppButton variant="secondary" onClick={() => navigate('/login')}>
                Back to Login
              </AppButton>
              <AppButton onClick={() => navigate('/forgot-password')}>
                Request New Link
              </AppButton>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
