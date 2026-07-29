import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { AppInput } from '../../../shared/components/forms';
import { Toast } from 'primereact/toast';
import { useRef } from 'react';
import { useResetPasswordMutation } from '../queries';
import { PageHeader, AppButton } from '../../../shared/components/ui';

export default function ResetPasswordPage() {
  const [email, setEmail] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const toast = useRef<Toast>(null);
  const navigate = useNavigate();
  const resetMutation = useResetPasswordMutation();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (!email.trim() || !newPassword.trim()) {
      setError('All fields are required.');
      return;
    }

    if (newPassword !== confirmPassword) {
      setError('Passwords do not match.');
      return;
    }

    if (newPassword.length < 8) {
      setError('Password must be at least 8 characters.');
      return;
    }

    try {
      await resetMutation.mutateAsync({ email, newPassword });
      toast.current?.show({
        severity: 'success',
        summary: 'Password Reset',
        detail: 'Your password has been reset successfully.',
      });
      setTimeout(() => navigate('/login'), 2000);
    } catch {
      setError('Failed to reset password. Please try again.');
    }
  };

  return (
    <div style={{ maxWidth: 480, margin: '60px auto', padding: '0 20px' }}>
      <Toast ref={toast} />
      <PageHeader title="Reset Password" subtitle="Set your new password" />

      <div className="card" style={{ padding: 32 }}>
        <form onSubmit={handleSubmit}>
          <div className="form-field">
            <label>Email Address</label>
            <AppInput
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="Enter your email"
              style={{ width: '100%' }}
              type="email"
            />
          </div>

          <div className="form-field">
            <label>New Password</label>
            <AppInput
              value={newPassword}
              onChange={(e) => setNewPassword(e.target.value)}
              placeholder="Enter new password"
              style={{ width: '100%' }}
              type="password"
            />
          </div>

          <div className="form-field">
            <label>Confirm Password</label>
            <AppInput
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              placeholder="Confirm new password"
              style={{ width: '100%' }}
              type="password"
            />
          </div>

          {error && (
            <p style={{ color: 'var(--danger)', fontSize: 13, marginBottom: 12 }}>{error}</p>
          )}

          <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, marginTop: 24 }}>
            <AppButton variant="secondary" onClick={() => navigate('/login')}>
              Back to Login
            </AppButton>
            <AppButton type="submit" loading={resetMutation.isPending}>
              Reset Password
            </AppButton>
          </div>
        </form>
      </div>
    </div>
  );
}
