import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../auth';
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
        <div className="login-form-wrapper">
          <div className="login-form-header">
            <h2>Welcome back</h2>
            <p>Sign in to your account to continue</p>
          </div>

          <form onSubmit={handleSubmit} className="login-form">
            {error && (
              <div style={{ padding: '10px 14px', borderRadius: 'var(--radius-md)', background: 'rgba(239, 68, 68, 0.1)', color: '#ef4444', fontSize: 13, marginBottom: 8 }}>
                {error}
              </div>
            )}

            <div className="login-field">
              <label className="login-label">Username</label>
              <div className="login-input-wrapper">
                <i className="pi pi-user" />
                <input
                  type="text"
                  placeholder="Enter your username"
                  value={username}
                  onChange={(e) => setUsername(e.target.value)}
                  className="login-input"
                  required
                />
              </div>
            </div>

            <div className="login-field">
              <label className="login-label">Password</label>
              <div className="login-input-wrapper">
                <i className="pi pi-lock" />
                <input
                  type="password"
                  placeholder="Enter your password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  className="login-input"
                  required
                />
              </div>
            </div>

            <div className="login-options">
              <label className="login-checkbox">
                <input type="checkbox" />
                <span>Remember me</span>
              </label>
            </div>

            <button type="submit" className="login-btn" disabled={loading}>
              {loading ? (
                <span className="login-btn-loading">
                  <span className="spinner" />
                  Signing in...
                </span>
              ) : (
                <>
                  Sign In
                  <i className="pi pi-arrow-right" />
                </>
              )}
            </button>
          </form>

          <p className="login-footer-text">
            Government of India &middot; CM Fellow Program
          </p>
        </div>
      </div>
    </div>
  );
}
