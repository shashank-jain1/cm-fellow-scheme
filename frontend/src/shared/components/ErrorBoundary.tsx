import { Component, type ErrorInfo, type ReactNode } from 'react';

interface Props {
  children: ReactNode;
}

interface State {
  hasError: boolean;
  error: Error | null;
}

export default class ErrorBoundary extends Component<Props, State> {
  public state: State = {
    hasError: false,
    error: null,
  };

  public static getDerivedStateFromError(error: Error): State {
    return { hasError: true, error };
  }

  public componentDidCatch(error: Error, errorInfo: ErrorInfo) {
    console.error('Uncaught error in component tree:', error, errorInfo);
  }

  private handleReload = () => {
    window.location.reload();
  };

  private handleReset = () => {
    this.setState({ hasError: false, error: null });
  };

  public render() {
    if (this.state.hasError) {
      return (
        <div style={{
          minHeight: '60vh',
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          justifyContent: 'center',
          padding: '2rem',
          textAlign: 'center'
        }}>
          <div style={{
            background: 'var(--bg-surface, #ffffff)',
            border: '1px solid var(--border-light, #e0e0e0)',
            borderRadius: '12px',
            padding: '2.5rem',
            maxWidth: '500px',
            boxShadow: '0 4px 20px rgba(0, 0, 0, 0.08)'
          }}>
            <div style={{ fontSize: '3rem', marginBottom: '1rem', color: '#e53935' }}>
              <i className="pi pi-exclamation-triangle"></i>
            </div>
            <h2 style={{ marginBottom: '0.5rem', color: 'var(--text-color, #333)' }}>
              Something went wrong
            </h2>
            <p style={{ color: '#666', marginBottom: '1.5rem', fontSize: '0.95rem' }}>
              {this.state.error?.message || 'An unexpected application error occurred.'}
            </p>
            <div style={{ display: 'flex', gap: '1rem', justifyContent: 'center' }}>
              <button
                onClick={this.handleReset}
                className="p-button p-button-outlined p-button-secondary"
                style={{ padding: '0.6rem 1.2rem', borderRadius: '6px', cursor: 'pointer' }}
              >
                Try Again
              </button>
              <button
                onClick={this.handleReload}
                className="p-button p-button-primary"
                style={{ padding: '0.6rem 1.2rem', borderRadius: '6px', cursor: 'pointer' }}
              >
                Reload Page
              </button>
            </div>
          </div>
        </div>
      );
    }

    return this.props.children;
  }
}
