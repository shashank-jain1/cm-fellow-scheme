import { useParams, useNavigate } from 'react-router-dom';
import { Tag } from 'primereact/tag';
import { AppButton, SkeletonTable } from '../../../shared/components/ui';
import { formatDate } from '../../../shared/utils/format';
import { useTrainingSessions, useSessionMaterials } from '../queries';
import MaterialList from '../components/MaterialList';

export default function TrainingDetailPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const trainingScheduleId = Number(id);

  const { data: sessions, isLoading } = useTrainingSessions();
  const { data: materials, isLoading: materialsLoading } = useSessionMaterials(trainingScheduleId);

  const session = (sessions ?? []).find((s) => s.trainingScheduleId === trainingScheduleId);

  if (isLoading) {
    return <SkeletonTable columns={4} />;
  }

  if (!session) {
    return (
      <div className="card" style={{ padding: 40, textAlign: 'center' }}>
        <i className="pi pi-exclamation-circle" style={{ fontSize: '2.5rem', color: '#f59e0b', marginBottom: 16 }} />
        <h2>Training Not Found</h2>
        <p style={{ color: 'var(--text-muted)', marginBottom: 24 }}>The requested training could not be found.</p>
        <AppButton icon="pi pi-arrow-left" onClick={() => navigate('/training/list')}>
          Back to Activities
        </AppButton>
      </div>
    );
  }

  return (
    <div>
      <div className="page-header">
        <div>
          <div style={{ display: 'flex', alignItems: 'center', gap: 12, marginBottom: 4 }}>
            <AppButton variant="secondary" icon="pi pi-arrow-left" onClick={() => navigate('/training/list')}>
              Back
            </AppButton>
            <h1>{session.trainingTitle || `Training #${session.trainingScheduleId}`}</h1>
          </div>
          <p style={{ color: 'var(--text-muted)' }}>Training details and materials</p>
        </div>
        <Tag value={session.status} severity={session.status === 'completed' ? 'success' : 'info'} />
      </div>

      <div className="card" style={{ padding: 24, marginBottom: 20 }}>
        <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Training Information</h3>
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(180px, 1fr))', gap: 16 }}>
          <div>
            <label style={{ fontSize: 12, color: 'var(--text-muted)', display: 'block' }}>Date</label>
            <span style={{ fontWeight: 500 }}>{formatDate(session.date)}</span>
          </div>
          <div>
            <label style={{ fontSize: 12, color: 'var(--text-muted)', display: 'block' }}>Time</label>
            <span style={{ fontWeight: 500 }}>
              {session.startTime ? `${session.startTime} - ${session.endTime}` : '—'}
            </span>
          </div>
          <div>
            <label style={{ fontSize: 12, color: 'var(--text-muted)', display: 'block' }}>Mode</label>
            <span style={{ fontWeight: 500 }}>{session.mode || '—'}</span>
          </div>
          <div>
            <label style={{ fontSize: 12, color: 'var(--text-muted)', display: 'block' }}>Activity Type</label>
            <span style={{ fontWeight: 500 }}>{session.activityType}</span>
          </div>
        </div>
      </div>

      <div className="card" style={{ padding: 24 }}>
        <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Training Materials</h3>
        <MaterialList
          trainingScheduleId={trainingScheduleId}
          materials={materials ?? []}
          isLoading={materialsLoading}
        />
      </div>
    </div>
  );
}
