import { useState } from 'react';
import { Button } from 'primereact/button';
import { AppDialog, AppInput } from '../../../../shared/components/forms';
import { useResetPassword } from '../../queries/user-management';
import type { UserAccountListItem } from '../../types/user-management';

interface ResetPasswordDialogProps {
  visible: boolean;
  user: UserAccountListItem | null;
  onHide: () => void;
}

export default function ResetPasswordDialog({ visible, user, onHide }: ResetPasswordDialogProps) {
  const resetMutation = useResetPassword();
  const [newPassword, setNewPassword] = useState('');
  const [error, setError] = useState('');

  const handleSubmit = async () => {
    if (!user) return;
    if (!newPassword || newPassword.length < 8) {
      setError('Password must be at least 8 characters');
      return;
    }
    if (!/[A-Z]/.test(newPassword) || !/[a-z]/.test(newPassword) || !/[0-9]/.test(newPassword)) {
      setError('Password must include an uppercase letter, a lowercase letter and a digit');
      return;
    }
    setError('');
    try {
      await resetMutation.mutateAsync({ userAccountId: user.userAccountId, newPassword });
      setNewPassword('');
      onHide();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to reset password');
    }
  };

  const handleHide = () => {
    setNewPassword('');
    setError('');
    onHide();
  };

  return (
    <AppDialog header="Reset Password" visible={visible} onHide={handleHide} style={{ width: 440 }} modal>
      {user && (
        <div style={{ display: 'flex', flexDirection: 'column', gap: 18, paddingTop: 8 }}>
          <p style={{ color: 'var(--text-secondary)', fontSize: 14, margin: 0 }}>
            Resetting password for <strong>{user.username}</strong> ({user.emailId})
          </p>
          <div className="form-field">
            <label>New Password</label>
            <AppInput
              type="password"
              value={newPassword}
              onChange={(e) => setNewPassword(e.target.value)}
              placeholder="Enter new password"
            />
            {error && <small style={{ color: '#ef4444' }}>{error}</small>}
          </div>
        </div>
      )}
      <div style={{ display: 'flex', gap: 12, justifyContent: 'flex-end', marginTop: 24 }}>
        <Button label="Cancel" className="btn btn-secondary" onClick={handleHide} />
        <Button label="Reset Password" className="btn btn-primary" onClick={handleSubmit} loading={resetMutation.isPending} />
      </div>
    </AppDialog>
  );
}
