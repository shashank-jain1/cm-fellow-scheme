import React from 'react';
import { useNavigate } from 'react-router-dom';
import AppButton from '../../../shared/components/ui/AppButton';

export default function UnauthorizedPage() {
  const navigate = useNavigate();

  return (
    <div style={{
      minHeight: '70vh',
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
        padding: '3rem 2.5rem',
        maxWidth: '520px',
        boxShadow: '0 4px 20px rgba(0, 0, 0, 0.06)'
      }}>
        <div style={{ fontSize: '3.5rem', color: '#f59e0b', marginBottom: '1rem' }}>
          <i className="pi pi-lock"></i>
        </div>
        <h2 style={{ fontSize: '1.5rem', fontWeight: 600, color: 'var(--text-color, #1f2937)', marginBottom: '0.5rem' }}>
          Access Denied
        </h2>
        <p style={{ color: '#6b7280', fontSize: '0.95rem', lineHeight: '1.5', marginBottom: '2rem' }}>
          You do not have the required permissions to access this module or page. Please contact your system administrator if you believe this is an error.
        </p>
        <div style={{ display: 'flex', gap: '1rem', justifyContent: 'center' }}>
          <AppButton
            label="Go to Dashboard"
            icon="pi pi-home"
            onClick={() => navigate('/dashboard')}
          />
        </div>
      </div>
    </div>
  );
}
