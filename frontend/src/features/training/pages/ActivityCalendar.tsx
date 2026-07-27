import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { Tag } from 'primereact/tag';
import { useTrainingSessions } from '../queries';
import { formatDate } from '../../../shared/utils/format';

export default function ActivityCalendar() {
  const [searchTerm, setSearchTerm] = useState('');
  const navigate = useNavigate();
  const { data: schedules, isLoading } = useTrainingSessions();

  const filteredSchedules = (schedules ?? []).filter(
    (s) =>
      (s.trainingTitle ?? s.meetingTitle ?? '').toLowerCase().includes(searchTerm.toLowerCase()) ||
      s.activityType.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const statusSeverity = (status: string) => {
    switch (status) {
      case 'upcoming': return 'info';
      case 'ongoing': return 'success';
      case 'completed': return 'secondary';
      case 'cancelled': return 'danger';
      default: return 'secondary';
    }
  };

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Activity Calendar</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            View and manage training & meeting activities
          </p>
        </div>
        <Button
          label="New Activity"
          icon="pi pi-plus"
          className="btn btn-primary"
          onClick={() => navigate('/training/new')}
        />
      </div>

      <div style={{ display: 'flex', gap: 12, marginBottom: 24 }}>
        <div style={{ position: 'relative', flex: '0 0 320px' }}>
          <i className="pi pi-search" style={{ position: 'absolute', left: 12, top: '50%', transform: 'translateY(-50%)', color: 'var(--text-muted)' }} />
          <InputText
            value={searchTerm}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setSearchTerm(e.target.value)}
            placeholder="Search activities..."
            style={{ width: '100%', paddingLeft: 36 }}
          />
        </div>
      </div>

      {isLoading ? (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(340px, 1fr))', gap: 20 }}>
          {[1, 2, 3, 4, 5, 6].map((n) => (
            <div key={n} className="card" style={{ padding: 24 }}>
              <div className="skeleton" style={{ width: '70%', height: 18, marginBottom: 12 }} />
              <div className="skeleton" style={{ width: '50%', height: 14, marginBottom: 16 }} />
              <div className="skeleton" style={{ width: '100%', height: 14, marginBottom: 8 }} />
              <div className="skeleton" style={{ width: '60%', height: 14 }} />
            </div>
          ))}
        </div>
      ) : filteredSchedules.length > 0 ? (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(340px, 1fr))', gap: 20 }}>
          {filteredSchedules.map((schedule) => (
            <div key={schedule.trainingScheduleId} className="card" style={{ padding: 24, cursor: 'pointer' }}>
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', marginBottom: 12 }}>
                <h3 style={{ fontSize: 16, fontWeight: 600 }}>
                  {schedule.trainingTitle ?? schedule.meetingTitle}
                </h3>
                <Tag value={schedule.status} severity={statusSeverity(schedule.status)} />
              </div>
              <div style={{ display: 'flex', flexDirection: 'column', gap: 8, marginBottom: 16 }}>
                <div style={{ display: 'flex', alignItems: 'center', gap: 8, fontSize: 13, color: 'var(--text-secondary)' }}>
                  <i className="pi pi-tag" style={{ width: 16 }} /> {schedule.activityType}
                </div>
                <div style={{ display: 'flex', alignItems: 'center', gap: 8, fontSize: 13, color: 'var(--text-secondary)' }}>
                  <i className="pi pi-calendar" style={{ width: 16 }} /> {formatDate(schedule.date)}
                </div>
                <div style={{ display: 'flex', alignItems: 'center', gap: 8, fontSize: 13, color: 'var(--text-secondary)' }}>
                  <i className="pi pi-clock" style={{ width: 16 }} /> {schedule.startTime} - {schedule.endTime}
                </div>
                <div style={{ display: 'flex', alignItems: 'center', gap: 8, fontSize: 13, color: 'var(--text-secondary)' }}>
                  <i className="pi pi-video" style={{ width: 16 }} /> {schedule.mode}
                </div>
              </div>
            </div>
          ))}
        </div>
      ) : (
        <div className="card">
          <div className="empty-state">
            <i className="pi pi-calendar" />
            <h3>No activities found</h3>
            <p>Create a new activity to get started</p>
          </div>
        </div>
      )}
    </div>
  );
}
