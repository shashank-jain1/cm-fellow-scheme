import { Tag } from 'primereact/tag';
import { AppButton, EmptyState } from '../../../shared/components/ui';
import { formatDate } from '../../../shared/utils/format';
import type { TrainingScheduleDto, TrainingCompletion } from '../types';

interface TrainingCompletionTableProps {
  sessions: TrainingScheduleDto[];
  completions: TrainingCompletion[];
  onMark: (sessionId: number) => void;
}

export default function TrainingCompletionTable({ sessions, completions, onMark }: TrainingCompletionTableProps) {
  const getCompletionForSession = (sessionId: number) =>
    completions.filter((c) => c.trainingScheduleId === sessionId);

  if (sessions.length === 0) {
    return <EmptyState icon="pi pi-check-circle" title="No training sessions" description="Sessions will appear here once created" />;
  }

  return (
    <div className="table-wrapper">
      <table style={{ width: '100%', borderCollapse: 'collapse' }}>
        <thead>
          <tr>
            {['Training Title', 'Date', 'Mode', 'Status', 'Completions', 'Actions'].map((h) => (
              <th
                key={h}
                style={{
                  padding: '10px 16px',
                  textAlign: 'left',
                  fontSize: 12,
                  fontWeight: 600,
                  color: 'var(--text-muted)',
                  textTransform: 'uppercase',
                  letterSpacing: '0.04em',
                  borderBottom: '1px solid var(--border-color)',
                  background: 'var(--carbon-50)',
                }}
              >
                {h}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {sessions.map((session) => {
            const sessionCompletions = getCompletionForSession(session.trainingScheduleId);
            return (
              <tr key={session.trainingScheduleId} style={{ borderBottom: '1px solid var(--border)' }}>
                <td style={{ padding: '12px 16px', fontWeight: 600, fontSize: 14 }}>
                  {session.trainingTitle ?? session.meetingTitle ?? 'Untitled'}
                </td>
                <td style={{ padding: '12px 16px', fontSize: 13, color: 'var(--text-muted)' }}>
                  {formatDate(session.date)}
                </td>
                <td style={{ padding: '12px 16px', fontSize: 13, color: 'var(--text-muted)' }}>
                  {session.mode}
                </td>
                <td style={{ padding: '12px 16px' }}>
                  <Tag
                    value={session.status}
                    severity={
                      session.status === 'Completed' ? 'success' :
                      session.status === 'Ongoing' ? 'warning' :
                      session.status === 'Cancelled' ? 'danger' : 'info'
                    }
                  />
                </td>
                <td style={{ padding: '12px 16px', fontSize: 14 }}>
                  {sessionCompletions.length > 0 ? (
                    <span style={{ fontWeight: 600, color: 'var(--success)' }}>
                      {sessionCompletions.length} fellow(s)
                    </span>
                  ) : (
                    <span style={{ color: 'var(--text-muted)' }}>None</span>
                  )}
                </td>
                <td style={{ padding: '12px 16px' }}>
                  <AppButton
                    size="sm"
                    variant="ghost"
                    icon="pi pi-check"
                    onClick={() => onMark(session.trainingScheduleId)}
                  >
                    Mark
                  </AppButton>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}
