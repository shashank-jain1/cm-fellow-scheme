import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { AppInput } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';

interface ResetPasswordFormProps {
  onSubmit: (newPassword: string) => Promise<void>;
  isPending: boolean;
}

export default function ResetPasswordForm({ onSubmit, isPending }: ResetPasswordFormProps) {
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (!newPassword.trim()) {
      setError('Please enter a new password.');
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
    if (!/[A-Z]/.test(newPassword) || !/[a-z]/.test(newPassword) || !/[0-9]/.test(newPassword)) {
      setError('Password must include an uppercase letter, a lowercase letter and a digit.');
      return;
    }

    try {
      await onSubmit(newPassword);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to reset password. Please try again.');
    }
  };

  return (
    <form onSubmit={handleSubmit}>
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
          placeholder="Re-enter new password"
          style={{ width: '100%' }}
          type="password"
        />
      </div>

      {error && (
        <div style={{ color: 'var(--danger)', fontSize: 13, marginTop: 12 }}>{error}</div>
      )}

      <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, marginTop: 24 }}>
        <AppButton variant="secondary" onClick={() => navigate('/login')}>
          Back to Login
        </AppButton>
        <AppButton type="submit" loading={isPending}>
          Reset Password
        </AppButton>
      </div>
    </form>
  );
}
