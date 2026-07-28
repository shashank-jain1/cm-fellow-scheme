import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { InputText } from 'primereact/inputtext';
import { Toast } from 'primereact/toast';
import { useRef } from 'react';
import { useForgotPasswordMutation } from '../queries';
import { PageHeader, AppButton } from '../../../shared/components/ui';

export default function ForgotPasswordPage() {
  const [email, setEmail] = useState('');
  const toast = useRef<Toast>(null);
  const navigate = useNavigate();
  const forgotMutation = useForgotPasswordMutation();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!email.trim()) return;

    try {
      const result = await forgotMutation.mutateAsync(email);
      toast.current?.show({
        severity: 'success',
        summary: 'Email Sent',
        detail: result.data?.message || 'If an account exists, a reset link has been sent.',
      });
      setTimeout(() => navigate('/login'), 3000);
    } catch {
      toast.current?.show({
        severity: 'info',
        summary: 'Email Sent',
        detail: 'If an account exists with this email, a reset link has been sent.',
      });
      setTimeout(() => navigate('/login'), 3000);
    }
  };

  return (
    <div style={{ maxWidth: 480, margin: '60px auto', padding: '0 20px' }}>
      <Toast ref={toast} />
      <PageHeader title="Forgot Password" subtitle="Enter your email to reset your password" />

      <div className="card" style={{ padding: 32 }}>
        <form onSubmit={handleSubmit}>
          <div className="form-field">
            <label>Email Address</label>
            <InputText
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="Enter your registered email"
              style={{ width: '100%' }}
              type="email"
            />
          </div>

          <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, marginTop: 24 }}>
            <AppButton variant="secondary" onClick={() => navigate('/login')}>
              Back to Login
            </AppButton>
            <AppButton type="submit" loading={forgotMutation.isPending}>
              Send Reset Link
            </AppButton>
          </div>
        </form>
      </div>
    </div>
  );
}
