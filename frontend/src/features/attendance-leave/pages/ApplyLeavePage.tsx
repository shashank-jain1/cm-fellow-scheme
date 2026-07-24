import ApplyLeaveForm from '../components/ApplyLeaveForm';
import { useNavigate } from 'react-router-dom';

export default function ApplyLeavePage() {
  const navigate = useNavigate();

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Apply for Leave</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Submit a new leave application
          </p>
        </div>
      </div>

      <div
        style={{
          maxWidth: 640,
          padding: 24,
          border: '1px solid var(--border-color)',
          borderRadius: 12,
          background: 'var(--surface-card)',
        }}
      >
        <ApplyLeaveForm onSuccess={() => navigate('/leave/status')} />
      </div>
    </div>
  );
}
