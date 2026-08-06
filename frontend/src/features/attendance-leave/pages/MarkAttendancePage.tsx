import { useAuth } from '../../auth/useAuth';
import { useMarkAttendance, useAttendanceHistory } from '../queries';
import AttendanceCheckIn from './AttendanceCheckIn';
import AttendanceStatusCard from './AttendanceStatusCard';

export default function MarkAttendancePage() {
  const { user } = useAuth();
  const markAttendance = useMarkAttendance();
  const { data: todayRecords } = useAttendanceHistory();

  const hasCheckedIn = todayRecords && todayRecords.length > 0;
  const lastRecord = todayRecords?.[todayRecords.length - 1];

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Mark Attendance</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Capture your face and location to check in or check out
          </p>
        </div>
      </div>

      {markAttendance.isSuccess && (
        <div
          style={{
            padding: '12px 16px',
            background: 'var(--emerald-50)',
            border: '1px solid var(--emerald-200)',
            borderRadius: 8,
            marginBottom: 20,
            color: 'var(--emerald-700)',
            fontSize: 14,
          }}
        >
          Attendance marked successfully!
        </div>
      )}

      {markAttendance.isError && (
        <div
          style={{
            padding: '12px 16px',
            background: 'var(--red-50)',
            border: '1px solid var(--red-200)',
            borderRadius: 8,
            marginBottom: 20,
            color: 'var(--red-700)',
            fontSize: 14,
          }}
        >
          Failed to mark attendance. Please try again.
        </div>
      )}

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 24 }}>
        <AttendanceCheckIn userId={user?.userAccountId ?? 0} markAttendance={markAttendance} />

        <div>
          <h2 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16, color: 'var(--text-primary)' }}>
            Today's Attendance
          </h2>

          {hasCheckedIn && lastRecord ? (
            <AttendanceStatusCard
              record={lastRecord}
              userId={user?.userAccountId ?? 0}
              isPending={markAttendance.isPending}
            />
          ) : (
            <div className="empty-state">
              <i className="pi pi-clock" />
              <h3>No attendance yet</h3>
              <p>Capture your face to check in for today.</p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
