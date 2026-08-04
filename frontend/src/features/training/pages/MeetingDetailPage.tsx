import { useParams, useNavigate } from 'react-router-dom';
import { Tag } from 'primereact/tag';
import { AppButton, SkeletonTable } from '../../../shared/components/ui';
import { useTrainingMeetings } from '../queries';

export default function MeetingDetailPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const meetingId = Number(id);
  const { data: meetings, isLoading } = useTrainingMeetings();

  const meeting = (meetings ?? []).find((m) => m.trainingScheduleId === meetingId);

  if (isLoading) {
    return <SkeletonTable columns={4} />;
  }

  if (!meeting) {
    return (
      <div className="card" style={{ padding: 40, textAlign: 'center' }}>
        <i className="pi pi-exclamation-circle" style={{ fontSize: '2.5rem', color: '#f59e0b', marginBottom: 16 }} />
        <h2>Meeting Not Found</h2>
        <p style={{ color: 'var(--text-muted)', marginBottom: 24 }}>The requested meeting details could not be found.</p>
        <AppButton icon="pi pi-arrow-left" onClick={() => navigate('/training/meetings')}>
          Back to Meetings
        </AppButton>
      </div>
    );
  }

  return (
    <div>
      <div className="page-header">
        <div>
          <div style={{ display: 'flex', alignItems: 'center', gap: 12, marginBottom: 4 }}>
            <AppButton variant="secondary" icon="pi pi-arrow-left" onClick={() => navigate('/training/meetings')}>
              Back
            </AppButton>
            <h1>{meeting.meetingTitle || meeting.trainingTitle || `Meeting #${meeting.trainingScheduleId}`}</h1>
          </div>
          <p style={{ color: 'var(--text-muted)' }}>Meeting details and minutes</p>
        </div>
        <Tag value={meeting.status} severity={meeting.status === 'Completed' ? 'success' : 'info'} />
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '2fr 1fr', gap: 20 }}>
        <div className="card" style={{ padding: 24 }}>
          <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Meeting Information</h3>
          <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 16 }}>
            <div>
              <label style={{ fontSize: 12, color: 'var(--text-muted)', display: 'block' }}>Date</label>
              <span style={{ fontWeight: 500 }}>{new Date(meeting.date).toLocaleDateString()}</span>
            </div>
            <div>
              <label style={{ fontSize: 12, color: 'var(--text-muted)', display: 'block' }}>Time</label>
              <span style={{ fontWeight: 500 }}>{meeting.startTime ? `${meeting.startTime} - ${meeting.endTime}` : '—'}</span>
            </div>
            <div>
              <label style={{ fontSize: 12, color: 'var(--text-muted)', display: 'block' }}>Mode</label>
              <span style={{ fontWeight: 500 }}>{meeting.mode || 'In-Person'}</span>
            </div>
            <div>
              <label style={{ fontSize: 12, color: 'var(--text-muted)', display: 'block' }}>Project ID</label>
              <span style={{ fontWeight: 500 }}>#{meeting.projectId}</span>
            </div>
          </div>
        </div>

        <div className="card" style={{ padding: 24 }}>
          <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Actions</h3>
          <div style={{ display: 'flex', flexDirection: 'column', gap: 12 }}>
            <AppButton variant="secondary" icon="pi pi-upload">
              Upload MOM
            </AppButton>
            <AppButton variant="secondary" icon="pi pi-paperclip">
              Attach Files
            </AppButton>
          </div>
        </div>
      </div>
    </div>
  );
}
