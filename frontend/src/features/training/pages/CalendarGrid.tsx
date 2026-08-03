import { Tag } from 'primereact/tag';
import { AppButton } from '../../../shared/components/ui';
import { formatDate } from '../../../shared/utils/format';

const STATUS_TRANSITIONS: Record<string, string[]> = {
  Scheduled: ['Ongoing', 'Cancelled'], Ongoing: ['Completed', 'Cancelled'],
  Completed: ['Closed'], Closed: [], Cancelled: [],
};
const NEXT_STATUS_LABELS: Record<string, string> = { Ongoing: 'Start', Completed: 'Complete', Closed: 'Close', Cancelled: 'Cancel' };

function statusSeverity(status: string) {
  switch (status) {
    case 'Scheduled': return 'info'; case 'Ongoing': return 'success'; case 'Completed': return 'secondary';
    case 'Closed': return 'contrast'; case 'Cancelled': return 'danger'; default: return 'secondary';
  }
}

interface Props {
  schedules: any[];
  isLoading: boolean;
  isPending: boolean;
  onStatusChange: (id: number, status: string) => void;
}

export default function CalendarGrid({ schedules, isLoading, isPending, onStatusChange }: Props) {
  if (isLoading) {
    return (
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
    );
  }
  if (schedules.length === 0) return null;

  return (
    <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(340px, 1fr))', gap: 20 }}>
      {schedules.map((s) => {
        const allowed = STATUS_TRANSITIONS[s.status] ?? [];
        const nextStatus = allowed[0];
        return (
          <div key={s.trainingScheduleId} className="card" style={{ padding: 24 }}>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', marginBottom: 12 }}>
              <h3 style={{ fontSize: 16, fontWeight: 600 }}>{s.trainingTitle ?? s.meetingTitle}</h3>
              <Tag value={s.status} severity={statusSeverity(s.status)} />
            </div>
            <div style={{ display: 'flex', flexDirection: 'column', gap: 8, marginBottom: 16 }}>
              {[
                { icon: 'pi pi-tag', text: s.activityType },
                { icon: 'pi pi-calendar', text: formatDate(s.date) },
                { icon: 'pi pi-clock', text: `${s.startTime} - ${s.endTime}` },
                { icon: 'pi pi-video', text: s.mode },
              ].map((item, i) => (
                <div key={i} style={{ display: 'flex', alignItems: 'center', gap: 8, fontSize: 13, color: 'var(--text-secondary)' }}>
                  <i className={item.icon} style={{ width: 16 }} /> {item.text}
                </div>
              ))}
            </div>
            {allowed.length > 0 && (
              <div style={{ display: 'flex', gap: 8, borderTop: '1px solid var(--border-color)', paddingTop: 12 }}>
                {nextStatus && (
                  <AppButton size="sm" onClick={() => onStatusChange(s.trainingScheduleId, nextStatus)} loading={isPending}>
                    {NEXT_STATUS_LABELS[nextStatus] ?? nextStatus}
                  </AppButton>
                )}
                {allowed.includes('Cancelled') && s.status !== 'Scheduled' && (
                  <AppButton size="sm" variant="danger" onClick={() => onStatusChange(s.trainingScheduleId, 'Cancelled')} loading={isPending}>
                    Cancel
                  </AppButton>
                )}
              </div>
            )}
          </div>
        );
      })}
    </div>
  );
}
