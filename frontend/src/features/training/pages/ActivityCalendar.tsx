import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { AppInput } from '../../../shared/components/forms';
import { Tag } from 'primereact/tag';
import { Toast } from 'primereact/toast';
import { useRef } from 'react';
import { useTrainingSessions, useUpdateTrainingStatus } from '../queries';
import { formatDate } from '../../../shared/utils/format';
import { PageHeader, AppButton, EmptyState } from '../../../shared/components/ui';

const STATUS_TRANSITIONS: Record<string, string[]> = {
  Scheduled: ['Ongoing', 'Cancelled'],
  Ongoing: ['Completed', 'Cancelled'],
  Completed: ['Closed'],
  Closed: [],
  Cancelled: [],
};

const NEXT_STATUS_LABELS: Record<string, string> = {
  Ongoing: 'Start',
  Completed: 'Complete',
  Closed: 'Close',
  Cancelled: 'Cancel',
};

export default function ActivityCalendar() {
  const [searchTerm, setSearchTerm] = useState('');
  const navigate = useNavigate();
  const toast = useRef<Toast>(null);
  const { data: schedules, isLoading } = useTrainingSessions();
  const updateStatusMutation = useUpdateTrainingStatus();

  const filteredSchedules = (schedules ?? []).filter(
    (s) =>
      (s.trainingTitle ?? s.meetingTitle ?? '').toLowerCase().includes(searchTerm.toLowerCase()) ||
      s.activityType.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const statusSeverity = (status: string) => {
    switch (status) {
      case 'Scheduled': return 'info';
      case 'Ongoing': return 'success';
      case 'Completed': return 'secondary';
      case 'Closed': return 'contrast';
      case 'Cancelled': return 'danger';
      default: return 'secondary';
    }
  };

  const handleStatusChange = async (id: number, newStatus: string) => {
    try {
      await updateStatusMutation.mutateAsync({ trainingScheduleId: id, newStatus });
      toast.current?.show({
        severity: 'success',
        summary: 'Status Updated',
        detail: `Training status changed to ${newStatus}`,
      });
    } catch {
      toast.current?.show({
        severity: 'error',
        summary: 'Error',
        detail: 'Failed to update status',
      });
    }
  };

  return (
    <div>
      <Toast ref={toast} />
      <PageHeader
        title="Activity Calendar"
        subtitle="View and manage training & meeting activities"
        action={<AppButton onClick={() => navigate('/training/new')} icon="pi pi-plus">New Activity</AppButton>}
      />

      <div style={{ marginBottom: 24 }}>
        <AppInput
          value={searchTerm}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) => setSearchTerm(e.target.value)}
          placeholder="Search activities..."
          style={{ width: 320 }}
        />
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
          {filteredSchedules.map((schedule) => {
            const allowed = STATUS_TRANSITIONS[schedule.status] ?? [];
            const nextStatus = allowed[0];
            return (
              <div key={schedule.trainingScheduleId} className="card" style={{ padding: 24 }}>
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
                {allowed.length > 0 && (
                  <div style={{ display: 'flex', gap: 8, borderTop: '1px solid var(--border-color)', paddingTop: 12 }}>
                    {nextStatus && (
                      <AppButton
                        size="sm"
                        onClick={() => handleStatusChange(schedule.trainingScheduleId, nextStatus)}
                        loading={updateStatusMutation.isPending}
                      >
                        {NEXT_STATUS_LABELS[nextStatus] ?? nextStatus}
                      </AppButton>
                    )}
                    {allowed.includes('Cancelled') && schedule.status !== 'Scheduled' && (
                      <AppButton
                        size="sm"
                        variant="danger"
                        onClick={() => handleStatusChange(schedule.trainingScheduleId, 'Cancelled')}
                        loading={updateStatusMutation.isPending}
                      >
                        Cancel
                      </AppButton>
                    )}
                  </div>
                )}
              </div>
            );
          })}
        </div>
      ) : (
        <EmptyState icon="pi pi-calendar" title="No activities found" description="Create a new activity to get started" />
      )}
    </div>
  );
}
