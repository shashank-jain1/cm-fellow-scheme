import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../auth';
import LoginForm from './LoginForm';
import './LoginPage.css';

export default function LoginPage() {
  const navigate = useNavigate();
  const { login } = useAuth();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      await login(username, password);
      navigate('/dashboard');
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Login failed');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login-page">
      <div className="login-left">
        <div className="login-left-content">
          <div className="login-brand-icon">
            <i className="pi pi-shield" />
          </div>
          <h1 className="login-brand-title">CM Fellow</h1>
          <p className="login-brand-subtitle">Management System</p>
          <div className="login-features">
            <div className="login-feature">
              <i className="pi pi-check-circle" />
              <span>Streamlined Fellow Management</span>
            </div>
            <div className="login-feature">
              <i className="pi pi-check-circle" />
              <span>Real-time Attendance Tracking</span>
            </div>
            <div className="login-feature">
              <i className="pi pi-check-circle" />
              <span>Performance Analytics Dashboard</span>
            </div>
            <div className="login-feature">
              <i className="pi pi-check-circle" />
              <span>Certificate Generation & Verification</span>
            </div>
          </div>
        </div>
        <div className="login-left-decoration">
          <div className="decoration-circle c1" />
          <div className="decoration-circle c2" />
          <div className="decoration-circle c3" />
        </div>
      </div>

      <div className="login-right">
        <LoginForm
          username={username}
          setUsername={setUsername}
          password={password}
          setPassword={setPassword}
          loading={loading}
          error={error}
          onSubmit={handleSubmit}
        />
      </div>
    </div>
  );
}
