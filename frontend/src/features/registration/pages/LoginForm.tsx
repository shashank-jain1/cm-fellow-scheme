interface LoginFormProps {
  username: string;
  setUsername: (v: string) => void;
  password: string;
  setPassword: (v: string) => void;
  loading: boolean;
  error: string;
  onSubmit: (e: React.FormEvent) => void;
}

export default function LoginForm({
  username,
  setUsername,
  password,
  setPassword,
  loading,
  error,
  onSubmit,
}: LoginFormProps) {
  return (
    <div className="login-form-wrapper">
      <div className="login-form-header">
        <h2>Welcome back</h2>
        <p>Sign in to your account to continue</p>
      </div>

      <form onSubmit={onSubmit} className="login-form">
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
          <a href="/forgot-password" style={{ fontSize: 13, color: 'var(--accent-primary)' }}>
            Forgot Password?
          </a>
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
  );
}
