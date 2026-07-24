import { useQuery } from '@tanstack/react-query';
import { useState } from 'react';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { Tag } from 'primereact/tag';
import ApiService from '../../../services/ApiService';
import { formatDate } from '../../../shared/utils/format';

interface TrainingSession {
  id: number;
  title: string;
  trainer: string;
  date: string;
  startTime: string;
  endTime: string;
  location: string;
  status: string;
  enrolledCount: number;
  maxCapacity: number;
}

export default function TrainingCalendar() {
  const [searchTerm, setSearchTerm] = useState('');

  const { data: sessions, isLoading } = useQuery({
    queryKey: ['training-sessions'],
    queryFn: async () => {
      const res = await ApiService.get<TrainingSession[]>('training/sessions');
      return res.data ?? [];
    },
  });

  const filteredSessions = (sessions ?? []).filter(
    (s) =>
      s.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
      s.trainer.toLowerCase().includes(searchTerm.toLowerCase())
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
          <h1>Training Calendar</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            View and manage training sessions for CM Fellows
          </p>
        </div>
        <Button label="New Session" icon="pi pi-plus" className="btn btn-primary" />
      </div>

      <div style={{ display: 'flex', gap: 12, marginBottom: 24 }}>
        <div style={{ position: 'relative', flex: '0 0 320px' }}>
          <i className="pi pi-search" style={{ position: 'absolute', left: 12, top: '50%', transform: 'translateY(-50%)', color: 'var(--text-muted)' }} />
          <InputText
            value={searchTerm}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setSearchTerm(e.target.value)}
            placeholder="Search sessions..."
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
      ) : filteredSessions.length > 0 ? (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(340px, 1fr))', gap: 20 }}>
          {filteredSessions.map((session) => (
            <div key={session.id} className="card" style={{ padding: 24, cursor: 'pointer' }}>
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', marginBottom: 12 }}>
                <h3 style={{ fontSize: 16, fontWeight: 600 }}>{session.title}</h3>
                <Tag value={session.status} severity={statusSeverity(session.status)} />
              </div>
              <div style={{ display: 'flex', flexDirection: 'column', gap: 8, marginBottom: 16 }}>
                <div style={{ display: 'flex', alignItems: 'center', gap: 8, fontSize: 13, color: 'var(--text-secondary)' }}>
                  <i className="pi pi-user" style={{ width: 16 }} /> {session.trainer}
                </div>
                <div style={{ display: 'flex', alignItems: 'center', gap: 8, fontSize: 13, color: 'var(--text-secondary)' }}>
                  <i className="pi pi-calendar" style={{ width: 16 }} /> {formatDate(session.date)}
                </div>
                <div style={{ display: 'flex', alignItems: 'center', gap: 8, fontSize: 13, color: 'var(--text-secondary)' }}>
                  <i className="pi pi-clock" style={{ width: 16 }} /> {session.startTime} - {session.endTime}
                </div>
                <div style={{ display: 'flex', alignItems: 'center', gap: 8, fontSize: 13, color: 'var(--text-secondary)' }}>
                  <i className="pi pi-map-marker" style={{ width: 16 }} /> {session.location}
                </div>
              </div>
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', paddingTop: 12, borderTop: '1px solid var(--border-light)' }}>
                <span style={{ fontSize: 12, color: 'var(--text-muted)' }}>
                  {session.enrolledCount}/{session.maxCapacity} enrolled
                </span>
                <div style={{ width: 100, height: 4, borderRadius: 2, background: 'var(--navy-100)' }}>
                  <div
                    style={{
                      width: `${(session.enrolledCount / session.maxCapacity) * 100}%`,
                      height: '100%',
                      borderRadius: 2,
                      background: (session.enrolledCount / session.maxCapacity) > 0.8 ? 'var(--amber-500)' : 'var(--emerald-500)',
                    }}
                  />
                </div>
              </div>
            </div>
          ))}
        </div>
      ) : (
        <div className="card">
          <div className="empty-state">
            <i className="pi pi-calendar" />
            <h3>No training sessions found</h3>
            <p>Create a new training session to get started</p>
          </div>
        </div>
      )}
    </div>
  );
}
