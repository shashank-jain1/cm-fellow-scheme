import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { AppInput } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';

interface ResetPasswordFormProps {
  onSubmit: (email: string, newPassword: string) => Promise<void>;
  isPending: boolean;
}

export default function ResetPasswordForm({ onSubmit, isPending }: ResetPasswordFormProps) {
  const [email, setEmail] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

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
      await onSubmit(email, newPassword);
    } catch {
      setError('Failed to reset password. Please try again.');
    }
  };

  return (
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
        <p style={{ color: 'var(--danger)', fontSize: 13, marginBottom: 12 }}>
          {error}
        </p>
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
