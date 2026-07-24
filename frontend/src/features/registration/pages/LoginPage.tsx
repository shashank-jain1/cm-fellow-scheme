import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './LoginPage.css';

export default function LoginPage() {
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    // Simulated login
    setTimeout(() => {
      localStorage.setItem('token', 'demo-token');
      setLoading(false);
      navigate('/dashboard');
    }, 800);
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
            <div className="login-field">
              <label className="login-label">Email address</label>
              <div className="login-input-wrapper">
                <i className="pi pi-envelope" />
                <input
                  type="email"
                  placeholder="admin@cmfellow.gov.in"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
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
              <a href="#" className="login-forgot">Forgot password?</a>
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
